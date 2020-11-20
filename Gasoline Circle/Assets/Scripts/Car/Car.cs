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
    private float currentSpeed = 0f;

    public CarControllMode Mode { get => mode; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();            
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
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
        else if (Input.GetKey(KeyCode.S))
        {
            //Будущая заготовка к ловушке
            //капкан, масло, бомба
            //Возможно за очки
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
    }


    private void GetInputPlayerTwo()
    {
        float speed = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed = currentSpeed + movementSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(speed, -limitSpeed, limitSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        IInteractable interactableObj = collision.gameObject.GetComponent<IInteractable>();
        if (interactableObj != null)
        {
            interactableObj.Interact(this);
        }
    }
}
