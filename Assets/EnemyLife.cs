using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    
    public int Pvmax = 100;
    public int Pv = 100;
    public int AnimIDDead;

    public Animator HAMIDA;
    void Start()
    {
        
        AnimIDDead = Animator.StringToHash("Dead");
    }

    
    void Update()
    {
        if (Pv == 0)
        {
            HAMIDA.SetBool(AnimIDDead, true);
        }
    }
}
