using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс, отвечающий за игровой процесс (проверки условий победы)
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// Поле, в котором хранится экземпляр GameController
    /// </summary>
    private static GameController instance;

    /// <summary>
    /// Необходимое число очков для победы
    /// </summary>
    [SerializeField]
    private int winScore = 10;

    /// <summary>
    /// ScriptableObject, хранящий в себе счетчик побед
    /// для 2х игроков
    /// </summary>
    [SerializeField]
    private Settings settings;

    /// <summary>
    /// Текущие очки для игрока №1
    /// </summary>
    private int pointsOne = 0;
    /// <summary>
    /// Текущией очки для игрока №2
    /// </summary>
    private int pointsTwo = 0;

    


    /// <summary>
    /// Предсотавляет доступ из любого места кода
    /// к экземпляру GameController
    /// </summary>
    public static GameController Instance 
    {
        get 
        {
            return instance; 
        }
    }

    /// <summary>
    /// Свойство предоставляющее во вне значение
    /// количества очков игрока №1.
    /// </summary>
    public int PointsOne
    {
        get
        {
            return pointsOne;
        }
        private set
        {
            pointsOne = value;
            if (pointsOne < 0)
                WinForDestroy(1);
            else
                HUD.Instance.SetScore(1, pointsOne);

            if (pointsOne >= 10)
                WinForPoints(1);
        }
    }


    /// <summary>
    /// Свойство предоставляющее во вне значение
    /// количества очков игрока №2.
    /// </summary>
    public int PointsTwo 
    {
        get 
        {
            return pointsTwo;
        }
        private set 
        {
            pointsTwo = value;
            if (pointsTwo < 0)
                WinForDestroy(2);
            else
                HUD.Instance.SetScore(2, pointsTwo);

            if (pointsTwo >= 10)
                WinForPoints(2);
            
        }
    }

    /// <summary>
    /// Свойство редоставляет доступ к счетчику побед
    /// </summary>
    public Settings Settings { get => settings; }

    private void Awake()
    {
        if (ReferenceEquals(instance, null)) 
        {
            instance = this;
        }
    }


    private void Start()
    {
        //Начать воспроизведение музыки на уровне
        string nameClip = string.Format("{0}level_m", SceneManager.GetActiveScene().buildIndex);
        AudioController.Instance.PlayMusic(nameClip);
    }


    /// <summary>
    /// Уменьшение очков на 1 у выбранного игрока
    /// </summary>
    /// <param name="mode"></param>
    public void DecreaseThePlayerPoint(CarControllMode mode) 
    {
        switch (mode)
        {
            case CarControllMode.PlayerOne:
                PointsOne--;
                break;
            case CarControllMode.PlayerTwo:
                PointsTwo--;
                break;
        }
    }

    /// <summary>
    /// Увеличение очков у выбранного игрока
    /// </summary>
    /// <param name="mode"></param>
    public void IncreaseThePlayerPoint(CarControllMode mode) 
    {
        switch (mode) 
        {
            case CarControllMode.PlayerOne:
                PointsOne++;
                break;
            case CarControllMode.PlayerTwo:
                PointsTwo++;
                break;
        }
    }

    /// <summary>
    /// Логика победы по очкам. Вызывает окно победы 
    /// с соответствующим сообщением и прибавляет +1 к счетчику побед
    /// </summary>
    /// <param name="indexCar">Номер игрока</param>
    private void WinForPoints(int indexCar) 
    {
        switch (indexCar) 
        {
            case (1):
                HUD.Instance.ShowWinMenu("Поздравляем с победой по очкам игрока №1!");
                Settings.PlayerOneWins++;
                break;
            case (2):
                HUD.Instance.ShowWinMenu("Поздравляем с победой по очкам игрока №2!");
                Settings.PlayerTwoWins++;
                break;
        }
    }


    /// <summary>
    /// Логика победы за уничтожение противника. Вызывает окно победы 
    /// с соответствующим сообщением и прибавляет +1 к счетчику побед
    /// </summary>
    /// <param name="destroedCarIndex">Номер уничтоженного игрока</param>
    public void WinForDestroy(int destroedCarIndex) 
    {
        switch (destroedCarIndex) 
        {
            case (1):
                HUD.Instance.ShowWinMenu("Машина игрока №1 сломалась. Игрок №2 одержал победу");
                Settings.PlayerTwoWins++;
                break;
            case (2):
                HUD.Instance.ShowWinMenu("Машина игрока №2 сломалась. Игрок №1 одержал победу");
                Settings.PlayerOneWins++;
                break;
        }

    }

    /// <summary>
    /// Метод для загрузки следующего уровня
    /// </summary>
    public void LoadNextLevel() 
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index != 3)
            SceneManager.LoadScene(index + 1);
        else
            SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Перезапуск уровня
    /// </summary>
    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Выход в главно меню
    /// </summary>
    public void ExitToMainMenu() 
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void QuitGame() 
    {
        Application.Quit();
    }


    /// <summary>
    /// Освобождает ресуры при смене уровня
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }
}
