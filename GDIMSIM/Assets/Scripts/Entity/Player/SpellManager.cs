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

    private void Update()
    {
        if (Input.GetKeyDown(_playerControlMap[Settings.PlayerControls.FirstSpell]))
        {
            _equippedSpells[SlotNumber.FirstSlot].CastSpell();
        }
    }
}