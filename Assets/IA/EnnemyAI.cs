using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAI: MonoBehaviour
{
    public float statrun = 0f;
    private float distance;
    public Transform Target;
    public float VisionDistance = 10;
    public float Range = 2.2f;
    public float AttackTime = 1;
    private float attackTime;
    public  int DamageAmount;
    private UnityEngine.AI.NavMeshAgent navMesh;
    public  float Health;
    private bool isDead = false;
    public Animation animations;

    void Start() {
        navMesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animation>();
        attackTime = Time.time;
    }

    void Update() {
        if (!isDead) {
            Target = GameObject.Find("PlayerArmature").transform;

            distance = Vector3.Distance(Target.position, transform.position);

            if (distance < VisionDistance && distance > Range) {
                Chase();
                statrun = 3.5f;
                
            }

            if (distance < Range) {
                Attack();
            }
        }
    }

    void Chase() {
        //animations.Play("Run");
        navMesh.destination = Target.position;
    }

    void Attack() {
        navMesh.destination = transform.position;

        if (Time.time > attackTime) 
        {
            //animations.Play("punchrock");
            Target.GetComponent<LifeBar>().Damage(DamageAmount);
            Debug.Log(gameObject.name + " dealt " + DamageAmount + " HP to " + Target.gameObject.name);
            attackTime = Time.time + AttackTime;
        }
    }

    // idle
    public void ApplyDammage(float DamageAmount) {
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

    public void Kill() {
        isDead = true;
        //animations.Play("deadrock");
        Destroy(transform.gameObject, 5);
    }

}