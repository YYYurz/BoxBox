using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour,IMove
{
    [Header("主角色")]
    Transform player;
    [Header("角色生命值脚本")]
    PlayerHealth playerHealth;
    [Header("怪物生命值脚本")]
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        Move();
    }

    public void Move(Vector3 vector) { }

    //navmeshagent控制怪物向角色移动
    public void Move() {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            transform.LookAt(player);
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }

    public void PlayStepSound() { }

    public void WalkAnim() { }


}
