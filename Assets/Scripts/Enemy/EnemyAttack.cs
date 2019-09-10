using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour,IAttack
{
    [Header("每次攻击伤害")]
    public int attackDamage = 10;
    [Header("动画")]
    Animator anim;
    [Header("主角色")]
    GameObject player;
    [Header("怪物生命值脚本")]
    EnemyHealth enemyHealth;

    GameObject Attackable;

    bool playerInRange = false;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        playerInRange = false;
    }


    void Update()
    {
        //当Player在攻击范围内直接触发动画  伤害方法由事件调用
        if (playerInRange && enemyHealth.currentHealth > 0)
        {
            anim.SetBool("Attack", true);
        }

        if (!playerInRange)
        {
            anim.SetBool("Attack", false);
        }

        if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    //检测Player进入攻击范围
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            playerInRange = true;
            Attackable = other.gameObject;
        }

    }

    //检测Player离开攻击范围
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
            playerInRange = false;

    }

    //攻击方法  由怪物攻击动画的事件触发
    public void Attack()
    {
        if(Attackable.GetComponent<IHealth>() != null)
        {
            Attackable.GetComponent<IHealth>().TakeDamage(attackDamage);
        }
    }
}
