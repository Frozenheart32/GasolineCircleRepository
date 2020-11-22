using UnityEngine;


/// <summary>
/// Класс отвечающий за вопроизведение музыки и звуков
/// </summary>
public class AudioController : MonoBehaviour
{
    /// <summary>
    /// Поле, в котором хранится экземпляр AudioController
    /// </summary>
    private static AudioController instance;

    /// <summary>
    /// Источник музыки
    /// </summary>
    [SerializeField]
    private AudioSource musicSource;

    /// <summary>
    /// Набор музыки
    /// </summary>
    [SerializeField]
    private AudioClip[] musics;

    /// <summary>
    /// Музыка по умолчанию. На случай,
    /// если нет нужного трэка в наборе музыки
    /// </summary>
    [SerializeField]
    private AudioClip defaultMusic;

    /// <summary>
    /// Источник звуков
    /// </summary>
    [SerializeField]
    private AudioSource soundSource;

    //Массив звуков
    [SerializeField]
    private AudioClip[] sounds;

    /// <summary>
    /// Звук по умолчанию
    /// </summary>
    [SerializeField]
    private AudioClip defaultSound;

    public static AudioController Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Поиск звука в массиве
    /// </summary>
    /// <param name="clipName">Имя звука</param>
    /// <returns>Звук. Если звук не найден, возвращается значение переменной defaultClip</returns>
    private AudioClip GetSound(string clipName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == clipName)
            {
                return sounds[i];
            }
        }

        return defaultSound;
    }



    /// <summary>
    /// Воспроизведение звука из массива
    /// </summary>
    /// <param name="clipName">Имя звука</param>
    public void PlaySound(string clipName)
    {
        if (soundSource.isPlaying)
            soundSource.Stop();
        soundSource.PlayOneShot(GetSound(clipName));
    }



    /// <summary>
    /// Поиск музыки в массиве
    /// </summary>
    /// <param name="clipName">Имя файла с музыкой</param>
    /// <returns></returns>
    private AudioClip GetMusic(string clipName)
    {
        for (int i = 0; i < musics.Length; i++)
        {
            if (musics[i].name == clipName)
            {
                return musics[i];
            }
        }
        return defaultMusic;
    }



    /// <summary>
    /// Воспроизведение музыки из массива
    /// </summary>
    /// <param name="clipName">Имя музыки</param>
    public void PlayMusic(string clipName)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
        musicSource.clip = GetMusic(clipName);
        musicSource.Play();
    }



    /// <summary>
    /// Освобожданные данные при уничтожении
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }
}
