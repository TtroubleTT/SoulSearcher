using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Settings settings;
    
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
        _equippedSpells[number] = spellBase;
    }

    public Dictionary<SlotNumber, SpellBase> GetEquippedSpells()
    {
        return _equippedSpells;
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