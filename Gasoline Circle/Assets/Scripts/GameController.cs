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
                break;
            case (2):
                break;
        }
    }

    public void WinForDestroy() 
    {
        //Вызов окна с победой с уничтожением машины противника
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
