using UnityEngine;

/// <summary>
/// Абстрактный класс в котором реализован
/// интерфейс взаимодействия с машинами игроков.
/// Более конкретные условия находятся в наследниках
/// </summary>
public abstract class Trap : MonoBehaviour, IInteractable
{
    public virtual void Interact(Car car)
    {
        if (car.Mode == CarControllMode.PlayerOne)
        {
            GameController.Instance.DecreaseThePlayerPoint(CarControllMode.PlayerOne);
            GameController.Instance.IncreaseThePlayerPoint(CarControllMode.PlayerTwo);
            GameController.Instance.IncreaseThePlayerPoint(CarControllMode.PlayerTwo);
        }
        else if (car.Mode == CarControllMode.PlayerTwo)
        {
            GameController.Instance.DecreaseThePlayerPoint(CarControllMode.PlayerTwo);
            GameController.Instance.IncreaseThePlayerPoint(CarControllMode.PlayerOne);
            GameController.Instance.IncreaseThePlayerPoint(CarControllMode.PlayerOne);
        }
    }

}
