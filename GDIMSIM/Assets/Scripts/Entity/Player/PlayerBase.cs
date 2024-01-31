using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBase : EntityBase
{
    [Header("Player Stats")] 
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private Slider slider;
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }

    private void InitializeHealthBar()
    {
        slider.maxValue = MaxHealth;
        slider.value = CurrentHealth;
    }

    protected override void Awake()
    {
        base.Awake();
        InitializeHealthBar();
        // inventory = new Inventory();
        // uiInventory.SetInventory(inventory);
    }

    public override bool AddHealth(float amount)
    {
        bool addedHealth = base.AddHealth(amount);
        slider.value = CurrentHealth;
        return addedHealth;
    }
    
    public override bool SubtractHealth(float amount)
    {
        bool subtractedHealth = base.SubtractHealth(amount);
        slider.value = CurrentHealth;
        return subtractedHealth;
    }
    
    // Keep track of skill points, current souls, spells unlocked (some of ths might get its own class). If its just a number we can make it an abstracted number.
}
