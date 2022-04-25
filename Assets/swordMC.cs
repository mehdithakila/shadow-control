using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordMC : MonoBehaviour
{

    public bool jela;
    public LifeBar Life;
    public BoxCollider Collider;

    private void Update()
    {
        if(Life.vie == 0)
        {
            Collider.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("je le touche sa mere");
            jela = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("je le touche sa mere pas");
            jela = true;
        }
    }

}
