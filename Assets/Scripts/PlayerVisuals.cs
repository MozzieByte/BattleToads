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

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isOwned)
            return;

        Vector2 moveInput = context.ReadValue<Vector2>();

        if (moveInput.x > 0)
        {
            CmdFlipSprite(true);
        }
        else if (moveInput.x < 0)
        {
            CmdFlipSprite(false);
        }

        bool isMoving = moveInput.magnitude > 0;
        animator.SetBool("IsMoving", isMoving);
    }


    #region NETCODE

    [Command]
    private void CmdFlipSprite(bool state)
    {
        RpcFlipSprite(state);
    }

    [ClientRpc]
    private void RpcFlipSprite(bool state)
    {
        spriteRenderer.flipX = state;
    }

    #endregion
}
