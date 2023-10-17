using Epic.OnlineServices.Lobby;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbySettings : MonoBehaviour
{
    [SerializeField] private string AttributeKey;
    private TMP_InputField input;

    public void OnInputFieldUpdated(string value)
    {
        OnValueUpdated(value);
    }

    public void OnToggleUpdated(bool value)
    {
        OnValueUpdated(value);
    }

    public void OnValueUpdated(AttributeDataValue value)
    {
        if (int.TryParse(value.AsUtf8, out var parsedInt))
        {
            value = parsedInt;
        }
        if (bool.TryParse(value.AsUtf8, out var parsedBool))
        {
            value = parsedBool;
        }

        AttributeData foundAttribute = FrogLobby.Instance.lobbySettingsAttributes.Find(x => x.Key == AttributeKey);
        if (foundAttribute != null)
        {
            foundAttribute.Value = value;
        }
        else
        {
            FrogLobby.Instance.lobbySettingsAttributes.Add(new Epic.OnlineServices.Lobby.AttributeData() { Key = AttributeKey, Value = value });
        }
    }

    /*public void OnChangeMaxPlayers(string value)
    {
        uint parsedValue = uint.Parse(value);
        if (parsedValue != default)
        {
            FrogLobby.Instance.maxPlayers = parsedValue;
        }
    }*/
}
