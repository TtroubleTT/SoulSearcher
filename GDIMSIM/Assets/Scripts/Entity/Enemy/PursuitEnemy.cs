using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitEnemy : EnemyBase
{
    // Contributors: Taylor
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth = 50f;

    [Header("References")]
    [SerializeField] private GameObject soul;
    [SerializeField] private GameObject player;
    private Transform _playerTransform;

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
        _playerTransform = player.transform;
    }

    private void Update()
    {
        
    }

    private void Pursue(Transform target)
    {
        Transform myTransform = transform;
        Vector3 targetDir = target.position - myTransform.position;

        float relativeHeading = Vector3.Angle(myTransform.forward, myTransform.TransformVector(target.forward));
    }
}