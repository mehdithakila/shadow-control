using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class LifeBar : MonoBehaviour
{
    public float vie = 100f;
    public float maxvie = 100f;

    public Image barvie;
    public TextMeshProUGUI nbrlife;

    void Update()
    {
        barvie.fillAmount = vie / maxvie;
        nbrlife.text = vie + "%";
        vie = Mathf.Clamp(vie, 0f, maxvie);
    }

    public void Damage(int damagerecu)
    {
        vie -= damagerecu;
    }
    public void Heal(int healrecu)
    {
        vie += healrecu;
    }
}

