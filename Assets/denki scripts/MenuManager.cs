using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public void PlayGame()
    {
        // Остановить музыку
        if (BackgroundMusic != null)
        {
            BackgroundMusic.Stop();
        }
        // Переход на сцену TestMVP
        SceneManager.LoadScene("game");
    }
    public void QuitGame()
    {
        // Завершить приложение
        Application.Quit();
        // Для отладки в Unity (чтобы увидеть, что кнопка работает)
        Debug.Log("Игра закрыта!");
    }
}
