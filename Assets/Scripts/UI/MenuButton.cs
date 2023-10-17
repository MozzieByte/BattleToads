using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [Tooltip("You do not need to set an OnClick listener on the button component to navigate to the set menu.")]
    [SerializeField] private Menu requestedMenu;

    public virtual void OnClick() { }

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        button.onClick.AddListener(Navigate);
    }

    private void Navigate()
    {
        if (requestedMenu != null)
            MenuManager.Instance.RequestMenu(requestedMenu);
    }
}
