using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private Menu previousMenu;

    public Menu GetPreviousMenu()
    {
        return previousMenu;
    }
}
