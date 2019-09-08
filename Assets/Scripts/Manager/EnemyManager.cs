using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [Header("怪物复活点")]
    public Transform[] SpawnPoint;
    [Header("每一关的僵尸数量")]
    public int[] LevelZombie = new int[10];
    [Header("每一关的蜘蛛数量")]
    public int[] LevelSpider = new int[10];
    [Header("玩家生命脚本")]
    public PlayerHealth playerHealth;
    [Header("僵尸预制件")]
    public GameObject Zombie;
    [Header("蜘蛛预制件")]
    public GameObject Spider;
    [Header("过关动画")]
    public Animator LevelAnimator;
    [Header("过关文本UI")]
    public Text LevelText;
    public static bool isWin = false;
    
    //过关音效
    AudioSource LevelAudio;
    //每只怪物生成间隔时间
    float Spawntime = 2f;
    //每一关当前生成的zombie和spider的数量
    int Z = 0, S = 0;
    //关卡
    int level = 0;
    //难度提升
    float SpawnDifficulty = 0.11f;
    
    //计时器
    float timer = 0f;

    //每一关怪物剩余数量
    public static int EnemyNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        isWin = false;
        Z = 0;
        S = 0;

        LevelAudio = GetComponent<AudioSource>();
        EnemyNum = LevelZombie[0] + LevelSpider[0];
        LevelAnimator.SetTrigger("Level");
        LevelAudio.Play();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= Spawntime)
        {
            Spawn();
            timer = 0f;
        }
        if (EnemyNum == 0 && level != 9)
        {
            LevelAudio.Play();
            Z = 0;
            S = 0;
            level++;

            Spawntime -= SpawnDifficulty;

            EnemyNum = LevelZombie[level] + LevelSpider[level];
            LevelText.text = "Level " + (level + 1);
            LevelAnimator.SetTrigger("Level");
        }
        if (level == 9 && EnemyNum == 0)
            isWin = true;
    }

    //怪物生成方法
    void Spawn()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }
        if (Random.Range(0, 1f) > 0.2)
        {
            if (Z < LevelZombie[level])
                SpawnZombie();
            else if (S < LevelSpider[level])
                SpawnSpider();
        }
        else
        {
            if (S < LevelSpider[level])
                SpawnSpider();
            else if (Z < LevelZombie[level])
                SpawnZombie();
        }

    }

    //生成僵尸
    void SpawnZombie()
    {
        Instantiate(Zombie, SpawnPoint[(int)Random.Range(0, 3.99f)].position, transform.rotation);
        Z++;
    }

    //生成蜘蛛
    void SpawnSpider()
    {
        Instantiate(Spider, SpawnPoint[(int)Random.Range(0, 3.99f)].position, transform.rotation);
        S++;
    }
}
