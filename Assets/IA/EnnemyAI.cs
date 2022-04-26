using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAI : MonoBehaviour
{
    public float statrun = 0f;
    private float distance;
    public Transform Target;
    public float VisionDistance = 10;
    public float Range = 2.2f;
    public float AttackTime = 1;
    private float attackTime;
    public int DamageAmount;
    private UnityEngine.AI.NavMeshAgent navMesh;
    public float Health;
    private bool isDead = false;
    public Animation animations;
    public EnemyLife life;

    public Animator QueueTapelle;
    private int PrivateIntEspaceUnderScore;
    private int UnAutrePrivateInt;

    void Start()
    {

        PrivateIntEspaceUnderScore = Animator.StringToHash("Ischassing");
        UnAutrePrivateInt = Animator.StringToHash("UnTrucOuIlAtteintLePerso");

        navMesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animation>();
        attackTime = Time.time;
    }

    void Update()
    {
        if (life.Pv > 0)
        {
            Target = GameObject.Find("PlayerArmature").transform;

            distance = Vector3.Distance(Target.position, transform.position);

            if (distance < VisionDistance && distance > Range)
            {
                Chase();
                statrun = 3.5f;

            }
            else if (distance >= VisionDistance)
            {
                QueueTapelle.SetBool(PrivateIntEspaceUnderScore, false);
            }

            if (distance < Range)
            {
                Attack();
            }
            else
            {
                QueueTapelle.SetBool(UnAutrePrivateInt, false);
            }
        }
    }

    void Chase()
    {
        Debug.Log("Jte vois sale arabe");
        QueueTapelle.SetBool(PrivateIntEspaceUnderScore, true);
        navMesh.destination = Target.position;
    }

    void Attack()
    {
        navMesh.destination = transform.position;
        QueueTapelle.SetBool(UnAutrePrivateInt, true);

        if (Time.time > attackTime)
        {

            Target.GetComponent<LifeBar>().Damage(DamageAmount);
            Debug.Log(gameObject.name + " dealt " + DamageAmount + " HP to " + Target.gameObject.name);
            attackTime = Time.time + AttackTime;
        }

    }

    // idle
    public void ApplyDammage(float DamageAmount)
    {
        /*
        if (!isDead) {
            Health -= DamageAmount;
            print(gameObject.name + "got hit for " + DamageAmount + " HP");

            if (Health <= 0) {
                Kill();
            }
        }
        */

    }

    public void Kill()
    {
        isDead = true;
        //animations.Play("deadrock");
        Destroy(transform.gameObject, 5);
    }



}