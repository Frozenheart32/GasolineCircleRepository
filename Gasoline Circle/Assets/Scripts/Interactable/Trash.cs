using UnityEngine;

/// <summary>
/// Класс описывающий кучку мусора.
/// При удале
/// </summary>
public class Trash : Trap
{
    public override void Interact(Car car)
    {
        base.Interact(car);
        car.HitOnTrash();
        //Звук разбрасывания мусора и вможно VFX

        Destroy(gameObject);
    }

    public static void CreateTrash(Transform placeForTrap) 
    {
        Instantiate(Resources.Load("Prefabs/InteractiveObjects/Trash"), placeForTrap.position, placeForTrap.rotation);
        //Звук создания ловуки


    }
}
