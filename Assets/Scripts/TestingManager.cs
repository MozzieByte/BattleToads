using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingManager : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private PlayerInputActions inputActions;

    private void Start()
    {
    }

    public override void OnStartClient()
    {
        if (!isOwned)
            return;

        inputActions = new PlayerInputActions();
        inputActions.Testing.Enable();
        inputActions.Testing.SpawnEnemy.performed += SpawnEnemy;
    }

    [Client]
    private void SpawnEnemy(InputAction.CallbackContext callback)
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        CmdSpawnEnemy(worldPosition);
    }

    [Command]
    private void CmdSpawnEnemy(Vector3 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);

        NetworkServer.Spawn(enemy);

        RpcSpawnEnemy();
    }

    [ClientRpc]
    private void RpcSpawnEnemy()
    {

    }
}
