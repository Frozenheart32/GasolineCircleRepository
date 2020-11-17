using UnityEngine;


/// <summary>
/// Класс отвечающий за хранение информации об игровом процессе между игровыми сценами
/// </summary>
[CreateAssetMenu(fileName = "NewSettings", menuName = "Create SettingsScrObj", order = 51)]
public class Settings : ScriptableObject
{
    /// <summary>
    /// Громкость аудио
    /// </summary>
    public float volume = 1f;

    /// <summary>
    /// Рекорд игрока №1
    /// </summary>
    public int PlayerOneHiScore = 0;
    /// <summary>
    /// Рекорд игрока №2
    /// </summary>
    public int PlayerTwoHiScore = 0;
}
