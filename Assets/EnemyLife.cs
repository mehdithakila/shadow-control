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

    private void _Died()
    {
        Died = true;
    }
    
    void Update()
    {
        HAMIDA.SetBool(AnimIDDied, Died);
        if (Pv <= 0)
        {
            HAMIDA.SetBool(AnimIDDead, true);
            Mahfoud.enabled = false;
        }
    }
}
