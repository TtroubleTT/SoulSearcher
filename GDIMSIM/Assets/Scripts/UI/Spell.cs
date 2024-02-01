using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public enum SpellType
    {
        SpellOne,
        SpellTwo,
        SpellThree,
        SpellFour,
        SpellFive,
    }

    public SpellType spellType;
    public int amount;

}