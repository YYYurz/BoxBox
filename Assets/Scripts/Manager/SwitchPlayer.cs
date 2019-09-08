using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    public GameObject Player;
    PlayerMove playerMove;
    PlayerHealth playerHealth;
    public GameObject[] Soldiers;
    public GameObject[] ShotEffect;
    public GameObject[] HeadUI;
    
    private int currentPlayer = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerMove = Player.GetComponent<PlayerMove>();
        playerHealth = Player.GetComponent<PlayerHealth>();
        ChangePlayer(currentPlayer);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePlayer(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangePlayer(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangePlayer(3);
        }
    }

    void ChangePlayer(int num)
    {
        for(int i = 0;i < Soldiers.Length;i++)
        {
            if(i == num)
            {
                playerMove.animator = Soldiers[i].GetComponent<Animator>();
                playerHealth.animator = Soldiers[i].GetComponent<Animator>();
                Soldiers[i].SetActive(true);
                ShotEffect[i].SetActive(true);
                HeadUI[i].SetActive(false);
            }
            else
            {
                Soldiers[i].SetActive(false);
                ShotEffect[i].SetActive(false);
                HeadUI[i].SetActive(true);
            }
        }
    }


}
