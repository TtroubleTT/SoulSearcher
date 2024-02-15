using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    // Contributors: Taylor
    [SerializeField] private Rigidbody rb;
    
    // Projectile Stats
    public enum Stats
    {
        Damage = 0,
        Speed = 1,
        Range = 2,
    }
    
    // Projectile Stats
    private float _damage;
    private float _speed;
    private float _range;
    
    // Physics
    private Vector3 _direction;

    private EnemyBase.StatusEffect _statusEffect;
    
    // Cooldown
    private float _lastHit = 0;

    private void Start()
    {
        // Lifespan of projectile
        Destroy(gameObject, _range / _speed);
    }

    public void ProjectileInitialize(Dictionary<Stats, float> stats, Vector3 direction, EnemyBase.StatusEffect effect)
    {
        _damage = stats[Stats.Damage];
        _speed = stats[Stats.Speed];
        _range = stats[Stats.Range];
        _direction = direction;
        _statusEffect = effect;
        
        ProjectileMove();
    }

    private void ProjectileMove()
    {
        rb.AddForce(_direction * _speed, ForceMode.Impulse);
    }

    // Consider keeping track of tag of who shoots to not do ff
    private void OnCollisionEnter(Collision other)
    {
        if (Time.time - _lastHit <= .5)
            return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerBase>().SubtractHealth(_damage);
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBase>().SubtractHealth(_damage);
            other.gameObject.GetComponent<EnemyBase>().CauseStatusEffect(_statusEffect);
        }
        
        _lastHit = Time.time;
        Destroy(gameObject);
    }
}