using System.Collections;
using UnityEngine;

/// <summary>
/// Класс отвечающий за управление авто
/// </summary>
public class Car : MonoBehaviour
{
    /// <summary>
    /// Место появление ловушек
    /// </summary>
    [SerializeField]
    private Transform placeForTrap;

    /// <summary>
    /// Вспомогательная точка следования авто
    /// </summary>
    [SerializeField]
    private Transform forwardPoint;
    /// <summary>
    /// скорость наращивания текущей скорости (ускорение)
    /// </summary>
    [SerializeField]
    private float movementSpeed = 1f;
    /// <summary>
    /// Ограничение скорости
    /// </summary>
    [SerializeField]
    private float limitSpeed = 3f;
    /// <summary>
    /// Коэффициент скольжения
    /// </summary>
    [SerializeField]
    private float coefSlip = 5f;
    /// <summary>
    /// Скорость вращения
    /// </summary>
    [SerializeField]
    private float rotateSpeed = 20f;
    /// <summary>
    /// Кто управляет машиной
    /// </summary>
    [SerializeField]
    private CarControllMode mode;

    /// <summary>
    /// Компонент физики авто
    /// </summary>
    private Rigidbody2D rigidbody;
    /// <summary>
    /// Текущая скорость авто
    /// </summary>
    [SerializeField]
    private float currentSpeed = 0f;
    /// <summary>
    /// Источник звука двигателя
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Свойство предоставляет доступ к информации
    /// о том кто управляет авто
    /// </summary>
    public CarControllMode Mode { get => mode; }


    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        //В зависимости от того кто управляет 
        //вызывает соответствующие проверки ввода
        if (Mode == CarControllMode.PlayerOne)
        {
            GetInputPlayerOne();
        }
        else
        {
            GetInputPlayerTwo();
        }
          
    }

    private void FixedUpdate()
    {
        MoveCar();
        RotateCar();
        CheckSoundCar();
    }

    /// <summary>
    /// Создание ловушки. Либо мусор, либо масло
    /// </summary>
    private void CreateTrash() 
    {
        int index = Random.Range(0, 100);

        if (index > 50)
            OilStain.CreateOilStain(placeForTrap);
        else
            Trash.CreateTrash(placeForTrap);

        if (Mode == CarControllMode.PlayerOne)
            GameController.Instance.DecreaseThePlayerPoint(CarControllMode.PlayerOne);
        else
            GameController.Instance.DecreaseThePlayerPoint(CarControllMode.PlayerTwo);
    }

    /// <summary>
    /// Просчет перемещения машины.
    /// </summary>
    private void MoveCar() 
    {
        Vector2 delta = new Vector2(forwardPoint.position.x, forwardPoint.position.y);
        rigidbody.MovePosition(
            Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), 
            delta, Time.deltaTime * currentSpeed));
    }
    
    /// <summary>
    /// Проверка ввода игрока №1
    /// </summary>
    private void GetInputPlayerOne() 
    {
        float speed = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            speed = currentSpeed + movementSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(speed, -limitSpeed, limitSpeed);
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= coefSlip * movementSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) 
        {
            if (GameController.Instance.PointsOne > 0)
                CreateTrash();
        }
    }

    /// <summary>
    /// Проверка ввода игрока №2
    /// </summary>
    private void GetInputPlayerTwo()
    {
        float speed = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed = currentSpeed + movementSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(speed, -limitSpeed, limitSpeed);
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= coefSlip * movementSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (GameController.Instance.PointsOne > 0)
                CreateTrash();
        }
    }

    /// <summary>
    /// Просчитывает поворот авто
    /// </summary>
    private void RotateCar()
    {
        float rotate = 0;

        if (Mode == CarControllMode.PlayerOne)
        {
            if (Input.GetKey(KeyCode.A))
                rotate = rotateSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.D))
                rotate = rotateSpeed * Time.deltaTime * -1f;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                rotate = rotateSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.RightArrow))
                rotate = rotateSpeed * Time.deltaTime * -1f;
        }
        Quaternion rotateAngle = Quaternion.Euler(0, 0, rotate);
        Quaternion currentAngle = transform.rotation * rotateAngle;
        rigidbody.MoveRotation(currentAngle);
    }

    /// <summary>
    /// Попадание в масло.
    /// </summary>
    public void HitOnOilStain() 
    {
        StartCoroutine(OiledCar());
    }

    /// <summary>
    /// Изменениние характеристик
    /// из-за попадания в масло
    /// </summary>
    /// <returns></returns>
    private IEnumerator OiledCar() 
    {
        float myRotateSpeed = rotateSpeed;
        rotateSpeed = 400f;
        currentSpeed /= 2;
        yield return new WaitForSeconds(3f);
        rotateSpeed = myRotateSpeed;
    }

    /// <summary>
    /// Попадание в мусор 
    /// и снижение скорости авто до 0
    /// </summary>
    public void HitOnTrash() 
    {
        currentSpeed = 0f;
    }

    /// <summary>
    /// Устанавливает тон и 
    /// грокость звука авто от скорости
    /// </summary>
    private void CheckSoundCar()
    {
        if (currentSpeed <= 1.5f)
        {
            audioSource.volume = 0.3f;
            audioSource.pitch = 1f;
        }
        else
        {
            audioSource.volume = 0.6f;
            audioSource.pitch = 1.3f;
        }
    }


    /// <summary>
    /// Реакция на столкновения
    /// </summary>
    /// <param name="collision">то с чем столкнулись</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IInteractable interactableObj = collision.gameObject.GetComponent<IInteractable>();
        if (interactableObj != null)
        {
            interactableObj.Interact(this);
        }
        else
        {
            AudioController.Instance.PlaySound("HitCar_s");
        }
    }
}
