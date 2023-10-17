using Epic.OnlineServices.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : Menu
{
    [SerializeField] private GameObject lobbyEntryTemplate;

    private void Start()
    {
        FrogLobby.Instance.PostFoundLobbySuccess += OnFoundLobbySuccess;
    }

    private void OnEnable()
    {
        FrogLobby.Instance.FindLobbies();
    }

    private void OnFoundLobbySuccess(object sender, FrogLobby.PostFoundLobbySuccessEventArgs e)
    {
        ClearLobbyEntries();

        foreach (LobbyDetails lobby in e.lobbies)
        {
            CreateLobbyEntry(lobby);
        }
    }

    private void OnDestroy()
    {
        FrogLobby.Instance.PostFoundLobbySuccess -= OnFoundLobbySuccess;
    }

    public void ClearLobbyEntries()
    {
        foreach (Transform child in lobbyEntryTemplate.transform.parent)
        {
            if (child != lobbyEntryTemplate.transform)
                Destroy(child.gameObject);
        }
    }

    public void CreateLobbyEntry(LobbyDetails lobbyDetails)
    {
        GameObject lobbyEntryInstance = Instantiate(lobbyEntryTemplate, lobbyEntryTemplate.transform.parent);
        lobbyEntryInstance.GetComponent<LobbyEntryUI>().Setup(lobbyDetails);
        lobbyEntryInstance.SetActive(true);
    }

    public void CreateLobby()
    {
        //FrogLobby.Instance.CreateLobby(4, LobbyPermissionLevel.Publicadvertised, false, new AttributeData[] { new AttributeData { Key = AttributeKeys[0], Value = lobbyName }, });
    }
}
