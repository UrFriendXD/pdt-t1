using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private CharacterMovement owner;

    private float mass;
    public float baseMass = 1f;
    public float baseCrashRadius = 1f;
    public float baseForce = 10f;
    public float movementDelay;
    public float destroyDelay = 2;
    
    [SerializeField] private float explodeDelay;
    Collider[] charsInRange = new Collider[0];
    private Collider bombCollider;
   
    // Start is called before the first frame update
    void Start()
    {
        mass = baseMass;
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(explodeDelay);
        charsInRange = Physics.OverlapSphere(transform.position, transform.localScale.x*baseCrashRadius * mass, LayerMask.GetMask("Player"));
            
        foreach (Collider c in charsInRange)
        {
            if (c)
            {
                Vector3 v = new Vector3(c.transform.position.x - transform.position.x, 0, c.transform.position.z - transform.position.z);
                Vector3 dir = v.normalized;
                float dist = v.magnitude;
                StartCoroutine(c.GetComponent<CharacterMovement>().PreventMovementForTime(movementDelay * 0.5f));
                c.attachedRigidbody.AddForce(dir * (baseForce * mass * (1f+1f/(0.25f+dist))),ForceMode.Force);
                Debug.Log(c.name);
            }
        }

        StartCoroutine(Reset());
        Debug.Log("exploded");
    }

    public void SetOwner(CharacterMovement player)
    {
        owner = player;
    }

    private IEnumerator Reset()
    {
        owner.ResetBomb();
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x * baseCrashRadius * mass);
    }
}
