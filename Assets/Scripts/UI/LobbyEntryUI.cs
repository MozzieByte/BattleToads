using Epic.OnlineServices.Lobby;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyEntryUI : MonoBehaviour
{
    private LobbyDetails lobbyDetails;

    [SerializeField] private TextMeshProUGUI lobbyName;
    [SerializeField] private TextMeshProUGUI playerCount;

    public void Setup(LobbyDetails lobby)
    {
        lobbyDetails = lobby;

        Attribute lobbyNameAttribute = new Attribute();
        lobby.CopyAttributeByKey(new LobbyDetailsCopyAttributeByKeyOptions { AttrKey = "lobby_name" }, out lobbyNameAttribute);
        if (lobbyNameAttribute != default(Attribute) && lobbyNameAttribute != null)
            lobbyName.text = lobbyNameAttribute.Data.Value.AsUtf8;

        string currentPlayerCount = lobby.GetMemberCount(new LobbyDetailsGetMemberCountOptions { }).ToString();

        Attribute maxConnectionsAttribute = new Attribute();
        lobby.CopyAttributeByKey(new LobbyDetailsCopyAttributeByKeyOptions { AttrKey = "max_connections" }, out maxConnectionsAttribute);
        string maxPlayerCount = "";
        if (maxConnectionsAttribute != default(Attribute) && maxConnectionsAttribute != null)
            maxPlayerCount = maxConnectionsAttribute.Data.Value.AsInt64.ToString();

        playerCount.text = $"{currentPlayerCount}/{maxPlayerCount}";
    }

    public void JoinLobby()
    {
        var attributeKeys = new List<string>();
        FrogLobby.Instance.lobbySettingsAttributes.ForEach(x => attributeKeys.Add(x.Key));
        FrogLobby.Instance.JoinLobby(lobbyDetails, attributeKeys.ToArray());
    }
}
