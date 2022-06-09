using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAI : MonoBehaviour
{
    public float statrun = 0f;
    public float distance;
    public Transform Target;
    public float VisionDistance = 10;
    public float Range = 2.2f;
    public float AttackTime = 1;
    private float attackTime;
    public int DamageAmount;
    public int DamageOffset;
    private UnityEngine.AI.NavMeshAgent navMesh;
    public float Health;
    private bool isDead = false;
    public EnemyLife life;
    public GameObject attackBox;

    public Animator QueueTapelle;
    private int _animIDIsChassing;
    private int _animIDAttack1;
    private int _animIDAttack2;
    private int rnd;

    void Start()
    {

        _animIDIsChassing = Animator.StringToHash("Ischassing");
        _animIDAttack1 = Animator.StringToHash("Attack1");
        _animIDAttack2 = Animator.StringToHash("Attack2");

        navMesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
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
                QueueTapelle.SetBool(_animIDIsChassing, false);
                statrun = 0;
            }

            if (distance < Range)
            {
                Attack();
            }
            else
            {
                QueueTapelle.SetBool(_animIDAttack1, false);
                QueueTapelle.SetBool(_animIDAttack2, false);
            }
        }
    }

    void Chase()
    {
        QueueTapelle.SetBool(_animIDIsChassing, true);
        navMesh.destination = Target.position;
    }

    void Attack()
    {
        navMesh.destination = transform.position;
        rnd = Random.Range(1,101);
        if (rnd <= 70)
        {
            QueueTapelle.SetBool(_animIDAttack1, true);
        }
        else
        {
            QueueTapelle.SetBool(_animIDAttack2, true);
        }
    }

    private void unAttack()
    {
        QueueTapelle.SetBool(_animIDAttack2, false);
        QueueTapelle.SetBool(_animIDAttack1, false);
        attackBox.SetActive(false);
    }



    // idle
    public void ApplyDammage()
    {
        attackBox.SetActive(true);
    }

    public void Kill()
    {
        isDead = true;
        Destroy(transform.gameObject, 5);
    }



}