using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,IHealth
{
    [Header("初始生命值")]
    public int startingHealth = 100;
    [Header("目前生命值")]
    public int currentHealth;
    [Header("生命值UI")]
    public Slider healthSlider;
    [Header("死亡音效")]
    public AudioSource DeathAudio;
    [Header("动画")]
    public Animator animator;
    
    bool isDead;
    bool damaged;


    void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Update()
    {
        healthSlider.value = currentHealth;
    }

    //被攻击
    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    //死亡
    public void Death()
    {
        isDead = true;
        
        animator.SetTrigger("Dead");

        DeathAudio.Play();
    }

    public void TakeGunShotDamage(int damage) { }

    public void SetHealth(int Health)
    {
        currentHealth += Health;
        if (currentHealth > 100)
            currentHealth = 100;
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
