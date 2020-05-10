using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement mover;
    private PlayerInput playerInput;
    [SerializeField] int index;

    //public PlayerController controls;

    private void Awake()
    {
        //controls = new PlayerController();
        playerInput = GetComponent<PlayerInput>();
    }
        

    // Start is called before the first frame update
    void Start()
    {
        index = playerInput.playerIndex;
        var movers = FindObjectsOfType<PlayerMovement>();
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (mover)
        {
            mover.SetMoveVector2(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (mover)
        {
            if (context.started)
            {
                mover.Jump();
            }
        }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (mover)
        {
            if (context.started)
            {
                mover.Action();
            }
            else if (context.canceled)
            {
                mover.UnAction();
            }
        }
    }
}
