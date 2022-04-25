using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] objects;
    
    public static DontDestroyOnLoad instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
        foreach (var VARIABLE in objects)
        {
            DontDestroyOnLoad(VARIABLE);
        }
    }
    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var VARIABLE in objects)
        {
            SceneManager.MoveGameObjectToScene(VARIABLE, SceneManager.GetActiveScene());
        }
    }

  
    
}
