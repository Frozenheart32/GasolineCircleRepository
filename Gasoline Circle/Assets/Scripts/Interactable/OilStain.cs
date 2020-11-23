using System.Collections;
using UnityEngine;

/// <summary>
/// Класс масленной ловушки
/// </summary>
public class OilStain : Trap
{
    /// <summary>
    /// коллайдер пятна
    /// </summary>
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        AudioController.Instance.PlaySound("OilOn_s");
        StartCoroutine(OilModeOn());
    }
    /// <summary>
    /// Чтобы создающий ловушку игрок сам 
    /// в нее сначала не вляпался дается задержка
    /// </summary>
    /// <returns></returns>
    private IEnumerator OilModeOn() 
    {
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }

    /// <summary>
    /// Срабатывание
    /// </summary>
    /// <param name="car">жертва</param>
    public override void Interact(Car car)
    {
        car.HitOnOilStain();
        AudioController.Instance.PlaySound("OilOff_s");
        Destroy(gameObject);
    }

    /// <summary>
    /// Создание ловушки
    /// </summary>
    /// <param name="placeForTrap">масто установки ловушки</param>
    public static void CreateOilStain(Transform placeForTrap)
    {
        Instantiate(Resources.Load("Prefabs/InteractiveObjects/OilStain"), placeForTrap.position, placeForTrap.rotation);
    }
}
