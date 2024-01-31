using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : SpellBase
{
    protected override float Cooldown { get; set; }

    [Header("Dashing")] 
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private float dashSpeed = 20;
    [SerializeField] private float dashDuration;
    [HideInInspector] public bool isDashing;
    private float _dashStartTime;
    private Vector3 _velocity;

    [Header("References")] 
    [SerializeField] private CharacterController controller;
    private PlayerMovement _playerMovement;

    protected override void InitializeAbstractedStats()
    {
        Cooldown = cooldown;
    }

    public override bool CastSpell()
    {
        (float x, float z) = GetHorizontalAndVerticalMovement(); // Tuple unpacking

        // If arent pressing a key (second needed command for dash) don't execute
        if (x == 0 && z == 0)
            return false;
        
        // Checks if cooldown has passed
        if (!base.CastSpell())
            return false;
        
        StartDash(x, z);
        return true;
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void Update()
    {
        base.Update();
        DashMove();
    }

    private (float, float) GetHorizontalAndVerticalMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return (x, z); // Tuple return
    }

    private Vector3 GetDashDirection(float x, float z)
    {
        Transform myTransform = transform;
        Vector3 direction = myTransform.right * x + myTransform.forward * z; // This makes it so its moving locally so rotation is taken into consideration
        return direction;
    }

    protected override void DoSpell()
    {
        isDashing = true;
        _playerMovement.dashSpeed = dashSpeed;
        _dashStartTime = Time.time;
    }

    private void StartDash(float x, float z)
    {
        Vector3 direction = GetDashDirection(x, z);
        _velocity = direction * dashSpeed;
    }

    private void DashMove()
    {
        if (isDashing)
        {
            // if its within the dash duration
            if (Time.time - _dashStartTime < dashDuration)
            {
                controller.Move(_velocity * Time.deltaTime);
            }
            else
            {
                isDashing = false;
            }
        }
    }
}
