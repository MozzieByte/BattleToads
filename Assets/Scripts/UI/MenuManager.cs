using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance {  get; private set; }

    public Menu currentMenu;

    private PlayerInputActions input;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        input = new PlayerInputActions();

        input.UI.Enable();
        input.UI.Cancel.performed += OnCancel;
    }

    private void OnCancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        RequestMenu(currentMenu.GetPreviousMenu());
    }

    public void RequestMenu(Menu newMenu)
    {
        if (newMenu != null)
        {
            if (currentMenu != null)
            {
                currentMenu.gameObject.SetActive(false);
            }

            newMenu.gameObject.SetActive(true);
            currentMenu = newMenu;
        }
        else
        {
            CloseCurrentMenu();
        }
    }

    public void CloseCurrentMenu()
    {
        currentMenu.gameObject.SetActive(false);
        currentMenu = null;
    }
}
