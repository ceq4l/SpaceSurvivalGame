using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler instance;

    public GameObject InventoryMenu;
    public GameObject MainUI;

    private void Awake() { instance = this; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenInventory();
    }

    void OpenInventory()
    {
        if (!InventoryMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            PlayerMovement.Instance.PauseInput = true;

            InventoryMenu.SetActive(true);
            MainUI.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.Instance.PauseInput = false;

            InventoryMenu.SetActive(false);
            MainUI.SetActive(true);
        }
    }
}