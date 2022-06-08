using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthshower : MonoBehaviour
{

    public EnemyLife me;
    void Update()
    {
        Debug.Log(me.Pv);
    }
}
