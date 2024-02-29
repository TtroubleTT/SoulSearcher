using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Close range attack
public class SoulStrike : MonoBehaviour, ICombat
{
    // Contributors: Taylor
    public float Damage { get; set; }

    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform cam;

    [Header("Attack")] 
    [SerializeField] private float damage = 50f;
    [SerializeField] private float attackDistance = 6f;
    [SerializeField] private float attackWidth = 2.5f;
    [SerializeField] private float cooldown = 1f;
    private float _lastAttack;

    private void Start()
    {
        Damage = damage;
    }

    public bool HurtEnemy(GameObject enemy)
    {
        if (!enemy.CompareTag("Enemy"))
            return false;
        
        enemy.GetComponent<EnemyBase>().SubtractHealth(Damage);
        return true;
    }

    public void Attack()
    {
        Vector3 camPos = cam.position;
        Quaternion camRot = cam.rotation;
        bool hitEnemy = Physics.BoxCast(camPos + (-cam.forward * 2.5f), new Vector3(attackWidth, attackWidth, attackWidth), cam.forward, out RaycastHit hitInfo, camRot, attackDistance, enemyLayer);
        if (hitEnemy)
        {
            HurtEnemy(hitInfo.transform.gameObject);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (Time.time - _lastAttack >= cooldown)
        {
            _lastAttack = Time.time;
            Attack();
        }
    }
}