using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public class CreateLobbyMenuButton : MenuButton
{
    public override void OnClick()
    {
        FrogLobby.Instance.CreateLobbyWithSettings();
    }
}
