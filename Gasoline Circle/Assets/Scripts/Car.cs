using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private Transform forwardPoint;

    [SerializeField]
    private float movementSpeed = 1f;

    [SerializeField]
    private float rotateSpeed = 20f;

    [SerializeField]
    private CarControllMode mode;


    private Rigidbody2D rigidbody;
    private float currentSpeed = 0f;

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
        if (mode == CarControllMode.PlayerOne)
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
        transform.position = Vector3.MoveTowards(transform.position, forwardPoint.position, Time.deltaTime * currentSpeed);
    }
    

    private void GetInputPlayerOne() 
    {
        float speed = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            speed = currentSpeed + movementSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(speed, -3f, 10f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Будущая заготовка к ловушке
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= movementSpeed * Time.deltaTime;
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
            currentSpeed = Mathf.Clamp(speed, -3f, 2f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= 5f * movementSpeed * Time.deltaTime;
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

        if (mode == CarControllMode.PlayerOne)
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

        transform.Rotate(new Vector3(0, 0, rotate), Space.Self);
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
