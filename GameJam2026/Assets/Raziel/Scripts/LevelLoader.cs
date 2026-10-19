using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index + 1);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitProgram()
    {
        Application.Quit();
    }
}
