using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Useraccountmanager : MonoBehaviour
{
    public static Useraccountmanager instance;
    public static string LoggedInUsername;
    public string lobbySceneName = "Lobby";
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    public void LogIn(Text username)
    {
        LoggedInUsername = username.text;
        SceneManager.LoadScene(lobbySceneName);

    }





}
