using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellEquipMenu : MonoBehaviour
{

    public static bool inventoryOpen = false;

    [SerializeField] GameObject spellEquipMenu;
    [SerializeField] GameObject InventoryCanvas;

    private void Start()
    {
        spellEquipMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (inventoryOpen)
            {
                Xbutton();
            }
            else
            {
                Pause();
            }
        }
    }
    private void Pause()
    {
        spellEquipMenu.SetActive(true);
        InventoryCanvas.SetActive(false);
        Time.timeScale = 0;
        inventoryOpen = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Xbutton()
    {
        spellEquipMenu.SetActive(false);
        Time.timeScale = 1;
        inventoryOpen = false;
    }
}
