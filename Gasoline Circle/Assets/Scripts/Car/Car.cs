using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{

    [SerializeField]
    private Transform placeForTrap;

    [SerializeField]
    private Transform forwardPoint;

    [SerializeField]
    private float movementSpeed = 1f;

    [SerializeField]
    private float limitSpeed = 3f;

    [SerializeField]
    private float coefSlip = 5f;

    [SerializeField]
    private float rotateSpeed = 20f;

    [SerializeField]
    private CarControllMode mode;


    private Rigidbody2D rigidbody;
    [SerializeField]
    private float currentSpeed = 0f;
    private AudioSource audioSource;

    public CarControllMode Mode { get => mode; }


    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    public void Update()
    {
        
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

    private void MoveCar() 
    {
        Vector2 delta = new Vector2(forwardPoint.position.x, forwardPoint.position.y);
        rigidbody.MovePosition(
            Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), 
            delta, Time.deltaTime * currentSpeed));
    }
    

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

    public void HitOnOilStain() 
    {
        StartCoroutine(OiledCar());
    }


    private IEnumerator OiledCar() 
    {
        float myRotateSpeed = rotateSpeed;
        rotateSpeed = 400f;
        currentSpeed /= 2;
        yield return new WaitForSeconds(3f);
        rotateSpeed = myRotateSpeed;
    }

    public void HitOnTrash() 
    {
        currentSpeed = 0f;
    }


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
