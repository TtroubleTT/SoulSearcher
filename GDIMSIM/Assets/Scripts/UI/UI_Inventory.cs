using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform spellSlotContainer;
    private Transform spellSlotTemplate;

    private void Awake()
    {
        spellSlotContainer = transform.Find("spellSlotContainer");
        spellSlotTemplate = spellSlotContainer.Find("spellSlotTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    private void RefreshInventorySpells()
    {
        int x = 0;
        int y = 0;
        float spellSlotCellSize = 30f;
        foreach (Spell spell in inventory.GetSpellList())
        {
            RectTransform spellSlotTransform = Instantiate(spellSlotTemplate, spellSlotContainer).GetComponent<RectTransform>();
            spellSlotRectTransform.gameObject.SetActive(true);
            spellSlotRectTransform.anchoredPosition = new Vector2(x * spellSlotCellSize, y * spellSlotCellSize);
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }

}
