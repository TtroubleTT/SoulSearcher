using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
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

    // Checks if they use one of their keybinds for casting an equip spell and cast the spell in that slot
    private void Update()
    {
        if (Input.GetKeyDown(settings.PlayerControlMap[Settings.PlayerControls.FirstSpell]))
        {
            _equippedSpells[SlotNumber.FirstSlot].CastSpell();
        }
        else if (Input.GetKeyDown(settings.PlayerControlMap[Settings.PlayerControls.SecondSpell]))
        {
            _equippedSpells[SlotNumber.SecondSlot].CastSpell();
        }
        else if (Input.GetKeyDown(settings.PlayerControlMap[Settings.PlayerControls.ThirdSpell]))
        {
            _equippedSpells[SlotNumber.ThirdSlot].CastSpell();
        }
        else if (Input.GetKeyDown(settings.PlayerControlMap[Settings.PlayerControls.ForthSpell]))
        {
            _equippedSpells[SlotNumber.ForthSlot].CastSpell();
        }
    }
}