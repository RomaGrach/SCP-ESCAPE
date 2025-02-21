using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public void PlayGame()
    {
        // ���������� ������
        if (BackgroundMusic != null)
        {
            BackgroundMusic.Stop();
        }
        // ������� �� ����� TestMVP
        SceneManager.LoadScene("game");
    }
    public void QuitGame()
    {
        // ��������� ����������
        Application.Quit();
        // ��� ������� � Unity (����� �������, ��� ������ ��������)
        Debug.Log("���� �������!");
    }
}
