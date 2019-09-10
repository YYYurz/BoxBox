using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IHealth
{
    [Header("护甲")]
    public int Armor = 0;
    [Header("起始生命值")]
    public int startingHealth = 100;
    [Header("实际生命值")]
    public int currentHealth;
    [Header("下沉速度")]
    public float sinkSpeed = 2.5f;
    [Header("分值")]
    public int scoreValue = 10;
    [Header("死亡音效")]
    public AudioClip deathClip;
    [Header("生命值UI")]
    public GameObject HealthCanvas;


    float TimeBetweenGunShotDamage = 0.7f;
    float timer = 0f;
    Animator anim;
    AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;
    SphereCollider sphereCollider;
    bool isDead;
    bool isSinking;

    //使用对象池
    ObjectPool Pool;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        Pool = ObjectPool.GetInstance();
        currentHealth = startingHealth;
        HealthCanvas.SetActive(true);
    }

    private void OnDisable()
    {
        isDead = false;

        capsuleCollider.enabled = true;
        sphereCollider.enabled = true;
        
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        isSinking = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    //被普通射击伤害
    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        
        currentHealth -= damage - Armor;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    //被击退攻击伤害
    public void TakeGunShotDamage(int amount)
    {
        if (isDead)
            return;

        if (timer >= TimeBetweenGunShotDamage)
        {
            //击中时将AI暂时无效化   造成刚体的受力效果
            GetComponent<EnemyMove>().enabled = false;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            Vector3 AttackMove = transform.position - GameObject.Find("Player").transform.position;
            AttackMove = AttackMove.normalized * 25f;
            GetComponent<Rigidbody>().AddForce(AttackMove, ForceMode.VelocityChange);

            GetComponent<EnemyMove>().enabled = true;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

            currentHealth -= amount - Armor;
            timer = 0;
        }
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    //死亡
    public void Death()
    {
        isDead = true;

        capsuleCollider.enabled = false;
        sphereCollider.enabled = false;

        anim.SetTrigger("Dead");
        
        enemyAudio.Play();
        
    }

    //当死亡动画触发时由动画触发事件   死亡动画播放完后尸体下沉并销毁
    public void StartSinking()
    {
        if (Random.Range(0, 10) < 2.8)
            Pool.GetObj("Supply", transform.position, transform.rotation);

        HealthCanvas.SetActive(false);
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        EnemyManager.EnemyNum--;
        StartCoroutine(Recovery());
    }

    public void SetHealth(int Health) { }

    public int GetHealth()
    {
        return currentHealth;
    }

    IEnumerator Recovery()
    {
        yield return new WaitForSeconds(0.7f);
        Pool.RecycleObj(gameObject);
    }
}
