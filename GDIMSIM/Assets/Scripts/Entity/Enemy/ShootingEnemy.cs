using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyBase
{
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth = 50f;
    [SerializeField] private float shotRange = 30f;
    [SerializeField] private float shotCooldown = 3f;
    private float _lastShotTime;

    [Header("Projectile Stats")] 
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 70f;
    
    [Header("References")] 
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject soul;
    [SerializeField] private Transform wandTransform;
    private GameObject _player;
    private Transform _playerTransform;

    private readonly Dictionary<ShootingProjectile.Stats, float> _projectileStats = new();

    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }

    private void InitializeStats()
    {
        _projectileStats.Add(ShootingProjectile.Stats.Damage, damage);
        _projectileStats.Add(ShootingProjectile.Stats.Speed, speed);
        _projectileStats.Add(ShootingProjectile.Stats.Range, range);
    }

    protected override void Die()
    {
        Instantiate(soul, gameObject.transform.position + Vector3.down, Quaternion.identity);
        base.Die();
    }

    private void Start()
    {
        InitializeStats();
        InitializeAbstractedStats();
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform;
    }

    private void Update()
    {
        Vector3 playerPos = _playerTransform.position;
        Vector3 lookPoint = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        transform.LookAt(lookPoint);
        CheckShoot();
    }

    // Checks if the distance between player and enemy is within the range they are allowed to fire
    private bool IsInRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance <= shotRange)
            return true;

        return false;
    }

    // Checks if the player is within the enemies line of sight
    private bool InLineOfSight()
    {
        if (Physics.Raycast(transform.position + transform.forward, (_player.transform.position - transform.position), out RaycastHit hitInfo, shotRange))
        {
            if (hitInfo.transform.gameObject == _player)
            {
                return true;
            }
        }

        return false;
    }

    // If the shot cooldown has passed, and the player is within shooting range, and line of sight then shoot
    private void CheckShoot()
    {
        if (Time.time - _lastShotTime > shotCooldown && IsInRange() && InLineOfSight())
        {
            Debug.Log("shoot");
            _lastShotTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        Transform myTransform = wandTransform;
        GameObject projectile = Instantiate(projectilePrefab, myTransform.position + (myTransform.forward * 2) + myTransform.up, myTransform.rotation);
        Vector3 direction = (_player.transform.position - transform.position).normalized; // Gets direction of player
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction, StatusEffect.None);
    }
}
