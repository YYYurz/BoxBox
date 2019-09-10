using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManger : MonoBehaviour
{
    public GameObject Player;
    public GameObject ShotEffects;
    public InputController inp;
    public Animator anim;

    ObjectPool Pool;

    // Start is called before the first frame update
    void Start()
    {
        Pool = ObjectPool.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        //胜利
        if(EnemyManager.isWin == true)
        {
            Pool.ClearAll();
            inp.enabled = false;
            Player.GetComponent<PlayerMove>().enabled = false;
            ShotEffects.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerMove>().au.Stop();
            anim.SetTrigger("Win");
        }
        //失败
        if(EnemyManager.isWin == false && Player.GetComponent<IHealth>().GetHealth() <= 0)
        {
            Pool.ClearAll();
            inp.enabled = false;
            Player.GetComponent<PlayerMove>().enabled = false;
            ShotEffects.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerMove>().au.Stop();
            anim.SetTrigger("GameOver");
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
