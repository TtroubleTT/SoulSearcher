using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PursuitEnemy : EnemyBase
{
    // Contributors: Taylor
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float healthToFlee = 50f;
    [SerializeField] private float distanceToPursue = 60f;

    [Header("References")]
    [SerializeField] private GameObject soul;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;

    [Header("Wander")]
    private Vector3 _wanderTarget = Vector3.zero;
    [SerializeField] private float wanderRadius = 10;
    [SerializeField] private float wanderDistance = 10;
    [SerializeField] private float wanderJitter = 1;

    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }

    protected override void Die()
    {
        Instantiate(soul, gameObject.transform.position + Vector3.down, Quaternion.identity);
        base.Die();
    }

    private void Start()
    {
        InitializeAbstractedStats();
    }

    private void Update()
    {
        AIState();
    }

    private void AIState()
    {
        if (CurrentHealth <= healthToFlee)
        {
            Flee(player.transform.position);
        }
        else if (IsInRange())
        {
            Pursue(player.transform);
        }
        else
        {
            Wander();
        }
    }
    
    // Checks if the distance between player and enemy 
    private bool IsInRange()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= distanceToPursue)
        {
            return true;
        }

        return false;
    }

    private void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    private void Flee(Vector3 location)
    {
        Vector3 myPos = transform.position;
        Vector3 fleeVector = location - myPos;
        agent.SetDestination(myPos - fleeVector);
    }

    private void Pursue(Transform target)
    {
        Transform myTransform = transform;
        Vector3 targetDir = target.position - myTransform.position;

        float relativeHeading = Vector3.Angle(myTransform.forward, myTransform.TransformVector(target.forward));
        float toTarget = Vector3.Angle(myTransform.forward, myTransform.TransformVector(targetDir));

        if (toTarget > 90 && relativeHeading < 20)
        {
            Seek(target.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / agent.speed;
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    private void Wander()
    {
        _wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter, 0, Random.Range(-1f, 1f) * wanderJitter);
        _wanderTarget.Normalize();
        _wanderTarget *= wanderRadius;

        Vector3 targetLocal = _wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);
        
        Seek(targetWorld);
    }
}