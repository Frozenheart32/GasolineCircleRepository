using UnityEngine;


/// <summary>
/// Класс отвечающий за хранение информации об игровом процессе между игровыми сценами
/// </summary>
[CreateAssetMenu(fileName = "NewSettings", menuName = "Create SettingsScrObj", order = 51)]
public class Settings : ScriptableObject
{
    /// <summary>
    /// Количество побед игрока №1
    /// </summary>
    public int PlayerOneWins = 0;
    /// <summary>
    /// Количество побед игрока №2
    /// </summary>
    public int PlayerTwoWins = 0;
}
