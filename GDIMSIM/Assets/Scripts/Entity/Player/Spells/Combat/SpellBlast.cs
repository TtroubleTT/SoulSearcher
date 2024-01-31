using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBlast : CombatSpell
{
    public override float Damage { get; set; }

    protected override float Cooldown { get; set; }

    [Header("References")] 
    [SerializeField] private Transform camTrans;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Stats")]
    [SerializeField] private float cooldown = 10f;
    [SerializeField] private float damage = 40f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 120f;
    
    private readonly Dictionary<ShootingProjectile.Stats, float> _projectileStats = new();

    protected override void InitializeAbstractedStats()
    {
        Damage = damage;
        Cooldown = cooldown;
    }
    
    private void InitializeStats()
    {
        _projectileStats.Add(ShootingProjectile.Stats.Damage, Damage);
        _projectileStats.Add(ShootingProjectile.Stats.Speed, speed);
        _projectileStats.Add(ShootingProjectile.Stats.Range, range);
    }
    
    private void Start()
    {
        InitializeStats();
    }

    protected override void DoSpell()
    {
        GameObject projectile = Instantiate(projectilePrefab, camTrans.position + (camTrans.forward * 2), camTrans.rotation);
        Vector3 direction = camTrans.forward.normalized; // Gets direction player is looking
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction);
    }

    protected override void Update()
    {
        base.Update();
    }
}