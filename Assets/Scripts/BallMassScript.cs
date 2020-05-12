using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMassScript : MonoBehaviour
{
    private float mass;
    public float baseMass = 1f;
    public float baseCrashRadius = 1f;
    public float baseForce = 10f;
    public float ballSpawnHeight = 20f;
    public float ballRespawnDelay = 2.0f;
    Collider[] charsInRange = new Collider[0];
    [SerializeField] private int count;

    public GameObject explosion;
    private SpriteRenderer explosionSR;
    public SpriteRenderer ballSR;
    public SpriteRenderer shadowSR;
    public PlayerMovement.PlayerNum lastHit;
    private Rigidbody ballRb;

    private void Awake()
    {
        ResetBall();
        ballRb = GetComponent<Rigidbody>();
        if (explosion)
        {
            explosionSR = explosion.GetComponent<SpriteRenderer>();
            explosionSR.enabled = false;
            ballSR.enabled = true;
            shadowSR.enabled = true;
        }
    }

    private void ResetBall()
    {
        count = 0;
        mass = baseMass;
    }

    public void IncrementCount()
    {
        count++;
        UpdateMass();
    }

    private void UpdateMass()
    {
        mass = baseMass * (1 + count/10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            charsInRange = Physics.OverlapSphere(transform.position, transform.localScale.x*baseCrashRadius * mass, LayerMask.GetMask("Player"));
            
            foreach (Collider c in charsInRange)
            {
                if (c)
                {
                    Vector3 v = new Vector3(c.transform.position.x - transform.position.x, 0, c.transform.position.z - transform.position.z);
                    Vector3 dir = v.normalized;
                    float dist = v.magnitude;
                    c.attachedRigidbody.AddForce(dir * baseForce * mass*(1f+1f/(0.25f+dist)),ForceMode.Force);
                    //StartCoroutine(c.GetComponent<CharacterMovement>().PreventMovementForTime(ballRespawnDelay * 0.5f));
                }
            }

            //AudioManager.Instance.PlayBall_Land();
            explosion.transform.localScale = 2f*Vector3.one * baseCrashRadius * mass;
            explosionSR.enabled = true;
            ballSR.enabled = false;
            shadowSR.enabled = false;
            ballRb.isKinematic = true;
            //ResetBall();
            //StartCoroutine(ResetBallPosition(collision.transform.position));
        }
    }

    /*public IEnumerator ResetBallPosition(Vector3 court)
    {
        yield return new WaitForSeconds(ballRespawnDelay);
        Vector3 spawnPos = court + Vector3.up * ballSpawnHeight;
        gameObject.transform.SetPositionAndRotation(spawnPos, transform.rotation);
        ballRb.velocity = Vector3.zero;
        explosionSR.enabled = false;
        ballSR.enabled = true;
        shadowSR.enabled = true;
        ballRb.isKinematic = false;
        yield return null;
    }*/

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x * baseCrashRadius * mass);
    }
}
