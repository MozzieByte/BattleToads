using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    private CharacterController characterController;

    [SyncVar]
    private int health;

    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private float speed = 5.0f;



    // Currently unused
    enum State
    {
        Idle,
        Aggro
    }

    private void Start()
    {
        health = maxHealth;
    }

    public override void OnStartClient()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!isServer)
            return;

        List<PlayerController> players = GameObject.FindObjectsOfType<PlayerController>().ToList();
        Transform closestPlayer = null;
        float currentSmallestDistance = Mathf.Infinity;
        Vector3 currentEnemyPosition = transform.position;
        foreach (PlayerController player in players)
        {
            float distanceFromPlayerToEnemy = Vector3.Distance(player.transform.position, currentEnemyPosition);
            if (distanceFromPlayerToEnemy < currentSmallestDistance)
            {
                closestPlayer = player.transform;
                currentSmallestDistance = distanceFromPlayerToEnemy;
            }
        }

        //Vector2 moveDirection = (closestPlayerLocation - (Vector2)transform.position).normalized;
        if (closestPlayer != null)
            Follow(closestPlayer.position);
    }


    [Server]
    private void Follow(Vector2 location)
    {
        if (!isServer)
            return;

        RpcFollow(location);
    }

    [ClientRpc]
    private void RpcFollow(Vector2 location)
    {
        Vector2 moveDirection = (location - (Vector2)transform.position).normalized;
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }

    [Command]
    public void CmdChangeHealth(int amount)
    {
        health += amount;
        RpcChangeHealth(amount);
    }

    [ClientRpc]
    private void RpcChangeHealth(int amount)
    {
        if (!isServer)
            health += amount;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
