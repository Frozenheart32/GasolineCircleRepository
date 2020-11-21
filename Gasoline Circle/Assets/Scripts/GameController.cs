using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    [SerializeField]
    private int winScore = 10;


    [SerializeField]
    private Settings settings;

    private int pointsOne = 0;
    private int pointsTwo = 0;

    



    public static GameController Instance 
    {
        get 
        {
            return instance; 
        }
    }

    public int PointsOne
    {
        get
        {
            return pointsOne;
        }
        set
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

    public int PointsTwo 
    {
        get 
        {
            return pointsTwo;
        }
        set 
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

    public Settings Settings { get => settings; }

    private void Awake()
    {
        if (ReferenceEquals(Instance, null)) 
        {
            instance = this;
        }
    }


    private void Start()
    {
        string nameClip = string.Format("{0}level_m", SceneManager.GetActiveScene().buildIndex);
        AudioController.Instance.PlayMusic(nameClip);
    }



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

    public void LoadNextLevel() 
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index != 3)
            SceneManager.LoadScene(index + 1);
        else
            SceneManager.LoadScene(1);
    }


    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ExitToMainMenu() 
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
