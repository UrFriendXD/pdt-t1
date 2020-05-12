using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterMovement charMovements;

    public enum PlayerNum { One, Two };

    public PlayerNum playerNum = PlayerNum.One;

    [SerializeField] private Vector2 move1;
    private Vector2 move2;

    private void Awake()
    {
        move1 = Vector2.zero;
    }

    public void SetMoveVector2(Vector2 m)
    {
        move1 = m;
        charMovements.SetMoveVector2(m);
    }

    public int GetPlayerIndex()
    {
        return (int)playerNum;
    }

    public void Action()
    {
        charMovements.Action();
    }

    public void UnAction()
    {
        charMovements.UnAction();
    }

    public void Jump()
    {
        charMovements.Jump();
    }
}
