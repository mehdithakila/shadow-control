using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class LifeBar : MonoBehaviour
{

    public float vie = 100;
    public int maxVie = 100;

    public Image barVie;
    public TextMeshProUGUI nbrLife;

    void Update()
    {
        barVie.fillAmount = vie / maxVie;
        nbrLife.text = vie + "%";
        vie = Mathf.Clamp(vie, 0f, maxVie);
    }

    public void Damage(int damageRecu)
    {
        vie -= damageRecu;
    }
    public void Heal(int healRecu)
    {
        vie += healRecu;
    }
}

