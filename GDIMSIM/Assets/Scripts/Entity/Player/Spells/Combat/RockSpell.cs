using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockSpell : CombatSpell
{
    // Contributors: Taylor, Amelia
    public override float Damage { get; set; }

    protected override float Cooldown { get; set; }

    protected override RawImage Icon { get; set; }

    [Header("References")]
    [SerializeField] private Transform camTrans;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Stats")]
    [SerializeField] private float cooldown = 10f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 120f;

    [SerializeField] private RawImage icon;

    private readonly Dictionary<ShootingProjectile.Stats, float> _projectileStats = new();

    protected override void InitializeAbstractedStats()
    {
        Damage = damage;
        Cooldown = cooldown;
        Icon = icon;
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
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction, EnemyBase.StatusEffect.None);
        
        GameObject projectile2 = Instantiate(projectilePrefab, camTrans.position + (camTrans.forward * 2) + camTrans.right, camTrans.rotation);
        Vector3 direction2 = camTrans.forward.normalized; // Gets direction player is looking
        projectile2.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction2, EnemyBase.StatusEffect.None);
        
        GameObject projectile3 = Instantiate(projectilePrefab, camTrans.position + (camTrans.forward * 2) - camTrans.right, camTrans.rotation);
        Vector3 direction3 = camTrans.forward.normalized; // Gets direction player is looking
        projectile3.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction3, EnemyBase.StatusEffect.None);
    }

    protected override void Update()
    {
        base.Update();
    }
}
