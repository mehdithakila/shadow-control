using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingWindow;
    public GameObject settingSolo;
    public GameObject settingOnline;

    public string Level;


    public void StartGame()
    {
        SceneManager.LoadScene(Level);
    }
    
    public void SettingsButton()
    {
       settingWindow.SetActive(true);
    }
    public void OnlineButton()
    {
        settingOnline.SetActive(true);
    }

    public void SoloButton()
    {
        settingSolo.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingWindow.SetActive(false);
    }
    public void CloseSoloWindow()
    {
        settingSolo.SetActive(false);
    }
    public void CloseOnlineWindow()
    {
        settingOnline.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
