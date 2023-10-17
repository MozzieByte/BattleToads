using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMenuButton : MenuButton
{
    [SerializeField] private Menu previousMenu;
    public override void OnClick()
    {
        MenuManager.Instance.RequestMenu(previousMenu);
    }
}
