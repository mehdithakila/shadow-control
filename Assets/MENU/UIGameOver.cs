using UnityEngine.SceneManagement; 
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public GameObject gameOverUI;

    public static UIGameOver instance;

    
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        
        gameOverUI.SetActive(false);
    }

    public void MenuButton()
    {
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
   
}
