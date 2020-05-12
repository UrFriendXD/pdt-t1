using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public PlayerSettings settings;

    private float speed;
    private float maxVel;
    private float groundDist;
    private float jumpForce;
    public enum CharState { Serving, Move, Set, Jump, Spike };

    public CharState charState = CharState.Move;

    private Vector3 velocity;
    private Vector3 movement;
    private Vector2 move;

    private Rigidbody myRb;

    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isGrounded = true;

    //[SerializeField] private AnimationController AC;

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform bombSpawnPoint;
    [SerializeField] private bool bombSpawned;


    private void Awake()
    {
        speed = settings.speed;
        maxVel = settings.maxVel;
        groundDist = settings.groundDist;
        jumpForce = settings.jumpForce;

        movement = Vector3.zero;

        myRb = GetComponent<Rigidbody>();

        //AC = GetComponentInChildren<AnimationController>();*/
    }

    void Update()
    {
        movement.x = move.x;
        movement.z = move.y;

        CalculateVelocity();

        SetGrounded();

        if (canMove)
        {
            //AC.SetRunAnim(move.magnitude);
            /*if(move.x>0 && !SR.flipX)
            {
                SR.flipX = !SR.flipX;
            }
            if (move.x < 0 && SR.flipX)
            {
                SR.flipX = !SR.flipX;
            }*/
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void SetMoveVector2(Vector2 m)
    {
        move = m;
    }
    
    private void Movement()
    {
        if (canMove)
        {
            myRb.MovePosition(myRb.position + velocity * Time.deltaTime);
        }
    }

    private void CalculateVelocity()
    {
        velocity = movement * speed;
        velocity = Vector3.ClampMagnitude(velocity, maxVel);
    }

    public void Action()
    {
        if (isGrounded && bombSpawned == false) //Maybe change it later so they can bomb in mid air
        {
            PlantBomb();
            bombSpawned = true;
        }
    }

    public void UnAction()
    {
        if (isGrounded)
        {
            AllowForMovement();
        }
        else
        {
            //hitBall.SetHitState(HittingBall.HitState.Nothing);
        }
    }

    private void PlantBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
        bomb.GetComponent<Bomb>().SetOwner(this);
    }

    public void ResetBomb()
    {
        bombSpawned = false;
    }

    public void AllowForMovement()
    {
        canMove = true;
        /*hitBall.SetHitState(HittingBall.HitState.Nothing);
        AC.SetIdleAnim();*/
    }

    public IEnumerator PreventMovementForTime(float time)
    {
        bool t = canMove;
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = t;
    }
    

    public void Jump()
    {
        if (canMove && isGrounded)
        {
            myRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            //AC.SetJumpAnim();
        }
    }

    private void SetGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);

        isGrounded = Physics.Raycast(ray, groundDist,LayerMask.GetMask("Ground"));

        if(!canMove && isGrounded && myRb.velocity.y < 0)
        {
            AllowForMovement();
            //AC.SetIdleAnim();
        }
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * groundDist);
    }

    public void Dead()
    {
        canMove = false;
        myRb.velocity = Vector3.zero;
    }

}
