using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    private Settings _settings;
    
    private Dictionary<SlotNumber, SpellBase> _equippedSpells = new();
    
    private Dictionary<Settings.PlayerControls, KeyCode> _playerControlMap = new ();

    public enum SlotNumber
    {
        FirstSlot,
        SecondSlot,
        ThirdSlot,
        ForthSlot,
    }

    public void UpdateSpellControls()
    {
        _playerControlMap = _settings.GetPlayerControls();
    }

    public void UpdateEquippedSpells(SlotNumber number, SpellBase spellBase)
    {
        _equippedSpells[number] = spellBase;
    }

    public Dictionary<SlotNumber, SpellBase> GetEquippedSpells()
    {
        return _equippedSpells;
    }

    private void Start()
    {
        _settings = GetComponent<Settings>();
        UpdateSpellControls();
    }

    // Checks if they use one of their keybinds for casting an equip spell and cast the spell in that slot
    private void Update()
    {
        if (Input.GetKeyDown(_playerControlMap[Settings.PlayerControls.FirstSpell]))
        {
            _equippedSpells[SlotNumber.FirstSlot].CastSpell();
        }
        else if (Input.GetKeyDown(_playerControlMap[Settings.PlayerControls.SecondSpell]))
        {
            _equippedSpells[SlotNumber.SecondSlot].CastSpell();
        }
        else if (Input.GetKeyDown(_playerControlMap[Settings.PlayerControls.ThirdSpell]))
        {
            _equippedSpells[SlotNumber.ThirdSlot].CastSpell();
        }
        else if (Input.GetKeyDown(_playerControlMap[Settings.PlayerControls.ForthSpell]))
        {
            _equippedSpells[SlotNumber.ForthSlot].CastSpell();
        }
    }
}