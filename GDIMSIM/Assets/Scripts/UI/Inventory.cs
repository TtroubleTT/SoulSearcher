using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Spell> spellList;

    public Inventory()
    {
        spellList = new List<Spell>();

        AddSpell(new Spell { spellType = Spell.SpellType.SpellOne, amount = 1 });
        AddSpell(new Spell { spellType = Spell.SpellType.SpellTwo, amount = 1 });
        AddSpell(new Spell { spellType = Spell.SpellType.SpellThree, amount = 1 });
        Debug.Log(spellList.Count);
    }

    public void AddSpell(Spell spell)
    {
        spellList.Add(spell);
    }

    public List<Spell> GetSpellList()
    {
        return spellList;
    }
}
