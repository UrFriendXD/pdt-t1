using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RespawnManager : MonoBehaviour
{
    #region Singleton
    private static RespawnManager _instance; 
    public static RespawnManager Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    #endregion

    [SerializeField] private GameObject[] SpawnPoints;

    [SerializeField] private int team1_Lives = 6;
    [SerializeField] private int team2_Lives = 6;

    [SerializeField] private Image[] team1_Hearts;
    [SerializeField] private Image[] team2_Hearts;

    [SerializeField] private Sprite dead;
    private static readonly int Dead = Animator.StringToHash("Dead");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn(Lives player)
    {
        //If a team loses their lives
        if (team1_Lives == 0)
        {
            //game over team 2 wins
        }
        
        if (team2_Lives == 0)
        {
            //game over team 1 wins
        }

        if (team1_Lives > 0 && team2_Lives > 0)
        {
            switch (player.playerID)
            {
                case 1:
                    player.transform.position = SpawnPoints[0].transform.position;
                    player.Died();
                    team1_Lives--;
                    team1_Hearts[team1_Lives].GetComponent<Animator>().SetTrigger(Dead);
                    break;
                case 2:
                    player.transform.position = SpawnPoints[1].transform.position;
                    player.Died();
                    team1_Lives--;
                    team1_Hearts[team1_Lives].GetComponent<Animator>().SetTrigger(Dead);
                    break;
                case 3:
                    player.transform.position = SpawnPoints[2].transform.position;
                    player.Died();
                    team2_Lives--;
                    team2_Hearts[team2_Lives].GetComponent<Animator>().SetTrigger(Dead);
                    break;
                case 4:
                    player.transform.position = SpawnPoints[3].transform.position;
                    player.Died();
                    team2_Lives--;
                    team2_Hearts[team2_Lives].GetComponent<Animator>().SetTrigger(Dead);
                    break;
            }
        }
    }

    public void ResetLives()
    {
        team1_Lives = 6;
        team1_Lives = 6;
    }
}
