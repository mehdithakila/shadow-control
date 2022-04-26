using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    
    public int Pvmax = 100;
    public int Pv = 100;
    public int AnimIDDead;
    public int AnimIDDied;
    public bool Died = false;

    public Animator HAMIDA;
    public Collider Mahfoud;
    void Start()
    {
        
        AnimIDDead = Animator.StringToHash("Dead");
        AnimIDDied = Animator.StringToHash("Died");
    }

    
    void Update()
    {
        HAMIDA.SetBool(AnimIDDied, Died);
        if (Pv <= 0)
        {
            HAMIDA.SetBool(AnimIDDead, true);
            Died = true;
            Mahfoud.enabled = false;
        }
    }
}
