using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    // Contributors: Taylor
    [Header("References")]
    [SerializeField] private Slider firstCooldown;
    [SerializeField] private Slider secondCooldown;
    [SerializeField] private Slider thirdCooldown;
    [SerializeField] private Slider forthCooldown;

    [Header("Icons")] 
    [SerializeField] private RawImage firstIcon;
    [SerializeField] private RawImage secondIcon;
    [SerializeField] private RawImage thirdIcon;
    [SerializeField] private RawImage forthIcon;

    [Header("Equipped Spells")]
    [SerializeField] private SpellBase first;
    [SerializeField] private SpellBase second;
    [SerializeField] private SpellBase third;
    [SerializeField] private SpellBase forth;
    
    private Dictionary<SlotNumber, SpellBase> _equippedSpells = new();

    public enum SlotNumber
    {
        FirstSlot,
        SecondSlot,
        ThirdSlot,
        ForthSlot,
    }

    public void UpdateEquippedSpells(SlotNumber number, SpellBase spellBase)
    {
        // Make this for every slider slot
        if (number == SlotNumber.FirstSlot)
        {
            spellBase.slider = firstCooldown;
            firstIcon.texture = spellBase.GetIcon().texture;
        }
        else if (number == SlotNumber.SecondSlot)
        {
            spellBase.slider = secondCooldown;
            secondIcon.texture = spellBase.GetIcon().texture;
        }
        else if (number == SlotNumber.ThirdSlot)
        {
            spellBase.slider = thirdCooldown;
            thirdIcon.texture = spellBase.GetIcon().texture;
        }
        else if (number == SlotNumber.ForthSlot)
        {
            spellBase.slider = forthCooldown;
            forthIcon.texture = spellBase.GetIcon().texture;
        }

        spellBase.slider.maxValue = spellBase.GetCooldown();
        spellBase.slider.value = spellBase.GetCooldown();
        _equippedSpells[number] = spellBase;
    }

    public Dictionary<SlotNumber, SpellBase> GetEquippedSpells()
    {
        return _equippedSpells;
    }

    private void Start()
    {
        UpdateEquippedSpells(SlotNumber.FirstSlot, first);
        UpdateEquippedSpells(SlotNumber.SecondSlot, second);
        UpdateEquippedSpells(SlotNumber.ThirdSlot, third);
        UpdateEquippedSpells(SlotNumber.ForthSlot, forth);
    }

    public void CastFirstSpell(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        _equippedSpells[SlotNumber.FirstSlot].CastSpell();
    }
    
    public void CastSecondSpell(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        _equippedSpells[SlotNumber.SecondSlot].CastSpell();
    }
    
    public void CastThirdSpell(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        _equippedSpells[SlotNumber.ThirdSlot].CastSpell();
    }
    
    public void CastForthSpell(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        _equippedSpells[SlotNumber.ForthSlot].CastSpell();
    }
}