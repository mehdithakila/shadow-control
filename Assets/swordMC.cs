using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordMC : MonoBehaviour
{
    public int damage = 20;
    public int offSet = 2;
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
            EnemyLife enemy = other.transform.GetComponent<EnemyLife>();
            enemy.Pv -= Random.RandomRange(damage - offSet, damage - offSet);
            Debug.Log("je le touche sa mere ce sale arabe");
            jela = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("je le touche sa mere pas ce sale arabe");
            jela = true;
        }
    }

}
