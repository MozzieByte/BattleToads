using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVisuals : NetworkBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;

    private Vector2 lastMoveDir = Vector2.zero;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isOwned)
            return;

        Vector2 moveInput = context.ReadValue<Vector2>();

        if (moveInput.x != 0 || moveInput.y != 0)
            lastMoveDir = moveInput;

        if (moveInput.x > 0)
        {
            FlipSprite(true);
        }
        else if (moveInput.x < 0)
        {
            FlipSprite(false);
        }

        bool isMoving = moveInput.magnitude > 0;
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MovingX", lastMoveDir.x);
        animator.SetFloat("MovingY", lastMoveDir.y);
    }

    [Client]
    private void FlipSprite(bool state)
    {
        if (!isLocalPlayer)
            return;

        spriteRenderer.flipX = state;

        CmdFlipSprite(state);
    }


    #region NETCODE

    [Command]
    private void CmdFlipSprite(bool state)
    {
        spriteRenderer.flipX = state;

        RpcFlipSprite(state);
    }

    [ClientRpc]
    private void RpcFlipSprite(bool state)
    {
        if (!isLocalPlayer)
            spriteRenderer.flipX = state;
    }

    #endregion
}
