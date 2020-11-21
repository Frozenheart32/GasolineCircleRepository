using System.Collections;
using UnityEngine;

public class OilStain : Trap
{
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        AudioController.Instance.PlaySound("OilOn_s");
        StartCoroutine(OilModeOn());
    }

    private IEnumerator OilModeOn() 
    {
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }

    public override void Interact(Car car)
    {
        //Вызвать функцию уменьшения трения и поворотливости у въехавшей машины
        //Звук масла
        //Возможно vfx с разбрызгиванием


        car.HitOnOilStain();
        AudioController.Instance.PlaySound("OilOff_s");
        Destroy(gameObject);
    }


    public static void CreateOilStain(Transform placeForTrap)
    {
        Instantiate(Resources.Load("Prefabs/InteractiveObjects/OilStain"), placeForTrap.position, placeForTrap.rotation);
    }
}
