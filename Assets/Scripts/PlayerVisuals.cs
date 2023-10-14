using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private PlayerLobbyData playerLobbyData;
    [SerializeField] private TextMeshProUGUI usernameText;

    private void Start()
    {
        if (playerLobbyData == null)
            return;
        if (usernameText == null)
            return;

        usernameText.text = playerLobbyData.PlayerName;
    }
}
