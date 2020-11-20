using UnityEngine;


/// <summary>
/// Класс отвечает за контрольные точки,
/// благодаря которым контролер этих точек
/// определяет, что игроки честно проходят уровень
/// </summary>
public class CheckPoint : MonoBehaviour
{
    
    /// <summary>
    /// Определяет какая это точка по счету
    /// </summary>
    [SerializeField]
    private CheckPointType type;

    /// <summary>
    /// Контроллер к которому относятся точки
    /// </summary>
    [SerializeField]
    private CheckPointController owner;


    private void Start()
    {
        owner = transform.GetComponentInParent<CheckPointController>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Car car = collision.GetComponent<Car>();
        if (!ReferenceEquals(car, null)) 
        {
            if (car.Mode == owner.Target) 
            {
                if (type == CheckPointType.Finish)
                {
                    owner.FinishDetected();
                }
                else 
                {
                    owner.SetResultCheck(type);
                }
            }
        }
    }
}
