using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform spellSlotContainer;
    private Transform spellSlotTemplate;
    [SerializeField] private GameObject spellSlot;

    private void Awake()
    {
        spellSlotContainer = GameObject.FindGameObjectWithTag("spellSlotContainer").transform;
        spellSlotTemplate = GameObject.FindGameObjectWithTag("spellSlotTemplate").transform;
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventorySpells();
    }

    private void RefreshInventorySpells()
    {
        Debug.Log("HEYYYY");
        int x = 0;
        int y = 0;
        float spellSlotCellSize = 30f;
        Debug.Log(inventory.GetSpellList().Count);
        foreach (Spell spell in inventory.GetSpellList())
        {
            Debug.Log("YOOOOOO");
            RectTransform spellSlotTransform = Instantiate(spellSlot, spellSlotTemplate.position, Quaternion.identity, spellSlotContainer).GetComponent<RectTransform>();
            Debug.Log("ughghg");
            spellSlotTransform.gameObject.SetActive(true);
            spellSlotTransform.anchoredPosition = new Vector2(x * spellSlotCellSize, y * spellSlotCellSize);
            //in video it is spellSlotRectTransform instead of spellSlotTransform
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
            Debug.Log("SQUADSDFSDF");
        }
    }

}
