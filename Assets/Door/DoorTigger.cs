using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTigger : MonoBehaviour
{
    [SerializeField]
    private trigerDoorController Door;


    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))
         {
             if (!Door.isOpen)
             {
                 Door.Open(other.transform.position);
             }
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Door.isOpen)
            {
                Door.Close();
            }
        }
    }
}
