using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    [SerializeField] Menu[] Menus;

    private void Awake()
    {
        Instance = this;

        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            if (Menus[i].menuName == menuName)
            {
                Menus[i].Open();
            }
            else if (Menus[i].open)
            {
                CloseMenu(Menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            if (Menus[i].open)
            {
                CloseMenu(Menus[i]);
            }
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
