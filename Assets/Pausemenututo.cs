using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenututo : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape");
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;

    }
    void Pause()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }
    
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
