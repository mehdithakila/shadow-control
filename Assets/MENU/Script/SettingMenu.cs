using UnityEngine.Audio;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetGameVolume(float volume)
    {
        audioMixer.SetFloat("GAME", volume);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MENU", volume);
    }
    

    public void SetFullScreen(bool isFullScreen) => Screen.fullScreen = isFullScreen;
}
