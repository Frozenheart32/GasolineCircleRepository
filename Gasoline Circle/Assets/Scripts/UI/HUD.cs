using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за UI на уровнях
/// </summary>
public class HUD : MonoBehaviour
{
    /// <summary>
    /// Поле хранящее в себе единственный экземпляр HUD
    /// </summary>
    private static HUD instance;

    /// <summary>
    /// Счетчик текущих очков игрока №1
    /// </summary>
    [SerializeField]
    private Text playerOneScoreText;

    /// <summary>
    /// Счетчик текущих очков игрока №2
    /// </summary>
    [SerializeField]
    private Text playerTwoScoreText;

    /// <summary>
    /// Счетчик побед игрока №1
    /// </summary>
    [SerializeField]
    private Text playerOneWinsText;

    /// <summary>
    /// Счетчик побед игрока №1
    /// </summary>
    [SerializeField]
    private Text playerTwoWinsText;

    /// <summary>
    /// Начальная панель с кнопкой Start перед гонкой
    /// </summary>
    [SerializeField]
    private GameObject startPanel;

    /// <summary>
    /// Окно победы
    /// </summary>
    [SerializeField]
    private GameObject winWindow;

    /// <summary>
    /// Сообщение всплывающее при победе
    /// </summary>
    [SerializeField]
    private Text winMessage;

    /// <summary>
    /// Окно паузы
    /// </summary>
    [SerializeField]
    private GameObject pauseWindow;

    /// <summary>
    /// Говорит о том, что заглушены ли звуки в игре
    /// </summary>
    private bool isMute = false;

    /// <summary>
    /// Свойство предоставляющее доступ 
    /// к единственному экземпляру HUD
    /// </summary>
    public static HUD Instance { get => instance; }


    private void Awake()
    {
        if (ReferenceEquals(instance, null))
            instance = this;
    }

    void Start()
    {
        ShowStartPanel();
        FillData();
    }

    private void Update()
    {
        //Вызов паузы
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ShowPauseWindow();
        }
    }

    /// <summary>
    /// Первоначальное заполнение данными
    /// </summary>
    private void FillData() 
    {
        playerOneScoreText.text = "0";
        playerTwoScoreText.text = "0";
        playerOneWinsText.text = GameController.Instance.Settings.PlayerOneWins.ToString();
        playerTwoWinsText.text = GameController.Instance.Settings.PlayerTwoWins.ToString();
    }

    /// <summary>
    /// Установка счетчиков игроков
    /// </summary>
    /// <param name="numberOfPlayer">Номер игрока</param>
    /// <param name="score">Количество очков</param>
    public void SetScore(int numberOfPlayer, int score) 
    {
        if (numberOfPlayer == 1)
        {
            playerOneScoreText.text = score.ToString();
        }
        else if (numberOfPlayer == 2) 
        {
            playerTwoScoreText.text = score.ToString();
        }
    }


    /// <summary>
    /// Метод, активирующий окно, показывающий курсор мыши,
    /// замедляющий игровое время.
    /// </summary>
    /// <param name="window"></param>
    private void ShowWindow(GameObject window)
    {
        Time.timeScale = 0.00001f;
        window.SetActive(true);
        Cursor.visible = true;
    }

    /// <summary>
    /// Метод скрывающий окно, восстанавливет течение игрового времени,
    /// скрывает курсор мыши
    /// </summary>
    /// <param name="window"></param>
    public void HideWindow(GameObject window)
    {
        window.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    /// <summary>
    /// Вызывает окно победы, с заданным сообщением
    /// </summary>
    /// <param name="message"></param>
    public void ShowWinMenu(string message) 
    {
        ShowWindow(winWindow);
        winMessage.text = message;
    }

    /// <summary>
    /// Вызывает окно паузы
    /// </summary>
    public void ShowPauseWindow() 
    {
        ShowWindow(pauseWindow);
    }

    /// <summary>
    /// Включает / выключает звук в игре
    /// </summary>
    public void MuteBtn() 
    {
        isMute = !isMute;
        if (isMute)
            AudioListener.volume = 0f;
        else
            AudioListener.volume = 1f;
    }

    /// <summary>
    /// Вызывает стартовую панель в начале гонки
    /// </summary>
    public void ShowStartPanel() 
    {
        ShowWindow(startPanel);
    }

    /// <summary>
    /// Вызывает загрузку следующего уровня
    /// </summary>
    public void NextBtn() 
    {
        GameController.Instance.LoadNextLevel();
    }

    /// <summary>
    /// Перезагрузка уровня
    /// </summary>
    public void RestartBtn() 
    {
        GameController.Instance.RestartGame();
    }

    /// <summary>
    /// ВЫход в главное меню
    /// </summary>
    public void ExitToMainManuBtn() 
    {
        GameController.Instance.ExitToMainMenu();
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitBtn() 
    {
        GameController.Instance.QuitGame();
    }

    /// <summary>
    /// Освобождает ресурсы при уничтожении
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }

}
