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

    public override void Interact(Car car)
    {
        base.Interact(car);
        car.HitOnTrash();
        //Звук разбрасывания мусора и вможно VFX
        AudioController.Instance.PlaySound("TrashOff_s");
        Destroy(gameObject);
    }

    public static void CreateTrash(Transform placeForTrap) 
    {
        Instantiate(Resources.Load("Prefabs/InteractiveObjects/Trash"), placeForTrap.position, placeForTrap.rotation);
        //Звук создания ловуки


    }
}
