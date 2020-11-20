using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private static HUD instance;


    [SerializeField]
    private Text playerOneScoreText;

    [SerializeField]
    private Text playerTwoScoreText;

    [SerializeField]
    private Text playerOneWinsText;

    [SerializeField]
    private Text playerTwoWinsText;

    [SerializeField]
    private GameObject winWindow;

    [SerializeField]
    private Text winMessage;

    [SerializeField]
    private GameObject pauseWindow;


    private bool isMute = false;
    public static HUD Instance { get => instance; }


    private void Awake()
    {
        if (ReferenceEquals(instance, null))
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FillData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ShowPauseWindow();
        }
    }

    private void FillData() 
    {
        playerOneScoreText.text = "0";
        playerTwoScoreText.text = "0";
        playerOneWinsText.text = GameController.Instance.Settings.PlayerOneWins.ToString();
        playerTwoWinsText.text = GameController.Instance.Settings.PlayerTwoWins.ToString();
    }



    private void ShowWindow(GameObject window)
    {
        Time.timeScale = 0.00001f;
        window.SetActive(true);
        Cursor.visible = true;
    }

    public void HideWindow(GameObject window)
    {
        window.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }


    public void ShowWinMenu(string message) 
    {
        ShowWindow(winWindow);
        winMessage.text = message;
    }

    public void ShowPauseWindow() 
    {
        ShowWindow(pauseWindow);
    }

    public void MuteBtn() 
    {
        isMute = !isMute;
        if (isMute)
            AudioListener.volume = 0f;
        else
            AudioListener.volume = 1f;
    }

    public void NextBtn() 
    {
        GameController.Instance.LoadNextLevel();
    }


    public void RestartBtn() 
    {
        GameController.Instance.RestartGame();
    }

    public void ExitToMainManuBtn() 
    {
        GameController.Instance.ExitToMainMenu();
    }

    public void ExitBtn() 
    {
        GameController.Instance.QuitGame();
    }

    private void OnDestroy()
    {
        instance = null;
    }

}
