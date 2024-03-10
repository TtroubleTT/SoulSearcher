using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellManagerOld : MonoBehaviour
{
    // Contributors: Taylor
    [Header("References")]
    [SerializeField] private Settings settings;
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
    
    private Dictionary<SlotNumber1, SpellBase> _equippedSpells = new();

    public enum SlotNumber1
    {
        FirstSlot,
        SecondSlot,
        ThirdSlot,
        ForthSlot,
    }

    public void UpdateEquippedSpells(SlotNumber1 number, SpellBase spellBase)
    {
        // Make this for every slider slot
        if (number == SlotNumber1.FirstSlot)
        {
            spellBase.slider = firstCooldown;
            firstIcon.texture = spellBase.GetIcon().texture;
        }
        else if (number == SlotNumber1.SecondSlot)
        {
            spellBase.slider = secondCooldown;
            secondIcon.texture = spellBase.GetIcon().texture;
        }
        else if (number == SlotNumber1.ThirdSlot)
        {
            spellBase.slider = thirdCooldown;
            thirdIcon.texture = spellBase.GetIcon().texture;
        }
        else if (number == SlotNumber1.ForthSlot)
        {
            spellBase.slider = forthCooldown;
            forthIcon.texture = spellBase.GetIcon().texture;
        }

        spellBase.slider.maxValue = spellBase.GetCooldown();
        spellBase.slider.value = spellBase.GetCooldown();
        _equippedSpells[number] = spellBase;
    }

    public Dictionary<SlotNumber1, SpellBase> GetEquippedSpells()
    {
        return _equippedSpells;
    }

    private void Start()
    {
        UpdateEquippedSpells(SlotNumber1.FirstSlot, first);
        UpdateEquippedSpells(SlotNumber1.SecondSlot, second);
        UpdateEquippedSpells(SlotNumber1.ThirdSlot, third);
        UpdateEquippedSpells(SlotNumber1.ForthSlot, forth);
    }

    // Checks if they use one of their keybinds for casting an equip spell and cast the spell in that slot
    private void Update()
    {
        if (Input.GetKeyDown(settings.PlayerControlMap[Settings.PlayerControls.FirstSpell]))
        {
            _equippedSpells[SlotNumber1.FirstSlot].CastSpell();
        }
        else if (Input.GetKeyDown(settings.PlayerControlMap[Settings.PlayerControls.SecondSpell]))
        {
            _equippedSpells[SlotNumber1.SecondSlot].CastSpell();
        }
        else if (Input.GetKeyDown(settings.PlayerControlMap[Settings.PlayerControls.ThirdSpell]))
        {
            _equippedSpells[SlotNumber1.ThirdSlot].CastSpell();
        }
        else if (Input.GetKeyDown(settings.PlayerControlMap[Settings.PlayerControls.ForthSpell]))
        {
            _equippedSpells[SlotNumber1.ForthSlot].CastSpell();
        }
    }
}
