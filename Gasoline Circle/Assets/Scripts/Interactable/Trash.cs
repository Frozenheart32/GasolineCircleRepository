using UnityEngine;

/// <summary>
/// Класс описывающий кучку мусора.
/// При удале
/// </summary>
public class Trash : Trap
{

    private void Start()
    {
        AudioController.Instance.PlaySound("TrashOn_s");
    }

    /// <summary>
    /// Взаимодействие с машиной
    /// </summary>
    /// <param name="car">машина</param>
    public override void Interact(Car car)
    {
        base.Interact(car);
        car.HitOnTrash();
        AudioController.Instance.PlaySound("TrashOff_s");
        Destroy(gameObject);
    }

    /// <summary>
    /// Создание мусора
    /// </summary>
    /// <param name="placeForTrap">место где мусор появится</param>
    public static void CreateTrash(Transform placeForTrap) 
    {
        Instantiate(Resources.Load("Prefabs/InteractiveObjects/Trash"), placeForTrap.position, placeForTrap.rotation);
    }
}
