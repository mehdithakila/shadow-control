using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class scriptchangementdescene : MonoBehaviour
{
    public string Level;
    public void StartGame()
    {
        SceneManager.LoadScene(Level);
    }


}
