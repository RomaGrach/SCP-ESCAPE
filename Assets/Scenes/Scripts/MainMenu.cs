using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Метод для кнопки "Играть"
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); 
    }

    // Метод для кнопки "Настройки"
    public void OpenSettings()
    {
        // Здесь можно открыть панель настроек или загрузить сцену настроек
        Debug.Log("Открыты настройки"); // Заглушка для примера
    }

    // Метод для кнопки "Выйти"
    public void QuitGame()
    {
        Application.Quit();
    }
}
