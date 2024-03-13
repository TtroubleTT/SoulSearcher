using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using kcp2k;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : EnemyBase
{
    // Contributors: Taylor
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float shotRange = 30f;
    [SerializeField] private float shotCooldown = 3f;
    [SerializeField] private float healthToHide = 50;
    private float _lastShotTime;

    [Header("Projectile Stats")] 
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 70f;

    [Header("References")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject soul;
    [SerializeField] private Transform wandTransform;
    [SerializeField] private NavMeshAgent agent;
    private GameObject _player;
    private List<GameObject> _hideableObjects = new();

    [Header("Wander")]
    private Vector3 _wanderTarget = Vector3.zero;
    [SerializeField] private float wanderRadius = 10;
    [SerializeField] private float wanderDistance = 10;
    [SerializeField] private float wanderJitter = 1;

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
        
        UpdatePlayerList();
        _player = GameObject.FindGameObjectWithTag("Player");
        _hideableObjects = GameObject.FindGameObjectsWithTag("Hide").ToList();
    }

    private void Update()
    {
        TargetPlayer();
        if (_player is null)
            return;
        
        Vector3 playerPos = _player.transform.position;
        Vector3 lookPoint = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        transform.LookAt(lookPoint);
        AiState();
    }
    
    private void TargetPlayer()
    {
        GameObject closestPlayer = _player;
        foreach (GameObject obj in _playerList)
        {
            if (_player is null)
            {
                _player = obj;
                return;
            }
            if (Vector3.Distance(transform.position, obj.transform.position) < Vector3.Distance(transform.position, closestPlayer.transform.position))
            {
                closestPlayer = obj;
            }
        }
        
        _player = closestPlayer;
    }

    private void AiState()
    {
        if (CurrentHealth <= healthToHide)
        {
            Hide();
        }
        else if (IsInRange())
        {
            CheckShoot();
        }
        else
        {
            Wander();
        }
    }

    // Checks if the distance between player and enemy is within the range they are allowed to fire
    private bool IsInRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        if (distance <= shotRange)
        {
            return true;
        }

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
        if (Time.time - _lastShotTime > shotCooldown && CanAttack && IsInRange() && InLineOfSight())
        {
            _lastShotTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        Transform myTransform = wandTransform;
        GameObject projectile = Instantiate(projectilePrefab, myTransform.position + (myTransform.forward * 2) + myTransform.up, myTransform.rotation);
        Vector3 direction = (_player.transform.position - transform.position).normalized; // Gets direction of player
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction, StatusEffect.None, "Enemy");
    }
    
    private void Wander()
    {
        _wanderTarget += new Vector3(UnityEngine.Random.Range(-1f, 1f) * wanderJitter, 0, UnityEngine.Random.Range(-1f, 1f) * wanderJitter);
        _wanderTarget.Normalize();
        _wanderTarget *= wanderRadius;

        Vector3 targetLocal = _wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);
        
        Seek(targetWorld);
    }
    
    private void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    private void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < _hideableObjects.Count; i++)
        {
            Vector3 hideDir = _hideableObjects[i].transform.position - _player.transform.position;
            Vector3 hidePos = _hideableObjects[i].transform.position + hideDir.normalized * 5;

            if (Vector3.Distance(transform.position, hidePos) < distance)
            {
                chosenSpot = hidePos;
                distance = Vector3.Distance(transform.position, hidePos);
            }
            
            Seek(chosenSpot);
        }
    }
}
