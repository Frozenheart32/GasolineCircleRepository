using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Класс отвечающий за логику главного меню
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Запускает 1-ый уровень
    /// </summary>
    public void StartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitGame() 
    {
        Application.Quit();
    }
}
