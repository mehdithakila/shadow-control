using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Menu",volume);
    }
    public void SetVolumeGame(float volume)
    {
        audioMixer.SetFloat("Game",volume);
    }

    //public void SetFullScreen(bool isFullScreen)
    //{
    //    Screen.fullscreen = isFullScreen;
    //}
}
