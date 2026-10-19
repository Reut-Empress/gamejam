using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
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
        //SceneManager.LoadScene(Level Select scene Index);
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
