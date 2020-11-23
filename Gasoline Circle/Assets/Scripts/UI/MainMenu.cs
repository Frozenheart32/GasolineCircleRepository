using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Класс отвечающий за логику главного меню
/// </summary>
public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        AudioController.Instance.PlayMusic("Menu_m");
    }


    /// <summary>
    /// Запускает 1-ый уровень
    /// </summary>
    public void StartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// ВЫзывает нужный уровень
    /// </summary>
    /// <param name="numberLevel">номер уровня</param>
    public void SelectLevel(int numberLevel) 
    {
        SceneManager.LoadScene(numberLevel);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitGame() 
    {
        Application.Quit();
    }
}
