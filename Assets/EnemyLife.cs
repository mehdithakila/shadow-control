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
    public StarterAssets.ThirdPersonController Target;
    public bool isShi = false;
    public bool isGS = false;
    public bool isBrw = false;
    public bool toShi = false;
    public bool toGS = false;
    public bool toBrw = false;
    public GameObject smoke;

    public Animator HAMIDA;
    public Collider Mahfoud;

    [Header("Skin")]
    public List<GameObject> everyCloathe;
    public List<GameObject> shinobiCloathes;
    public List<GameObject> GSCloathes;
    public List<GameObject> BrwCloathes;

    void Start()
    {
        AnimIDDead = Animator.StringToHash("Dead");
        AnimIDDied = Animator.StringToHash("Died");

        if (isShi)
        {
            GoShinobi();
        }
        else if (isGS)
        {
            GoGS();
        }
        else if (isBrw)
        {
            GoBrw();
        }
    }
    
    void Update()
    {
        HAMIDA.SetBool(AnimIDDied, Died);
        if (Pv <= 0 && !Died)
        {
            HAMIDA.SetBool(AnimIDDead, true);
            Mahfoud.enabled = false;
            smoke.SetActive(true);
            if (toShi)
            {
                GoShinobi();
            }
            else if (toGS)
            {
                GoGS();
            }
            else if (toBrw)
            {
                GoBrw();
            }
        }
        else
        {
            toShi = Target.IsShi;
            toGS = Target.IsGS;
            toBrw = Target.IsBrw;
        }
    }

    public void GoShinobi()
    {
        foreach (GameObject cloathe in everyCloathe)
        {
            cloathe.SetActive(false);
        }

        foreach (GameObject cloathe in shinobiCloathes)
        {
            cloathe.SetActive(true);
        }
    }

    public void GoGS()
    {
        foreach (GameObject cloathe in everyCloathe)
        {
            cloathe.SetActive(false);
        }

        foreach (GameObject cloathe in GSCloathes)
        {
            cloathe.SetActive(true);
        }
    }

    public void GoBrw()
    {
        foreach (GameObject cloathe in everyCloathe)
        {
            cloathe.SetActive(false);
        }

        foreach (GameObject cloathe in BrwCloathes)
        {
            cloathe.SetActive(true);
        }
    }

    private void _Died()
    {
        Died = true;
    }
}
