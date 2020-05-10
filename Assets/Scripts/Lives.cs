using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lives : MonoBehaviour
{
    //private int lives = 3;
    public int playerID;
    public int teamID;
    private CharacterMovement _characterMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void Died()
    {
        //RespawnManager.Instance.Respawn(this);
        _characterMovement.Dead(); //Disable controls
        //StartCoroutine(Timer());
    }

    /*
    private void Respawn()
    {
        //Re enable controls
        _characterMovement.Respawn();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        Respawn();
    }*/
}
