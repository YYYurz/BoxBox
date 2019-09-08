using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenadeShoot : MonoBehaviour,IShoot
{
    [Header("名称")]
    public string Name;
    [Header("子弹数量")]
    public int Ammo = 0;
    [Header("每枪间隔")]
    public float timeBetweenBullets = 0.25f;
    [Header("动画")]
    public Animator ani;
    [Header("榴弹速度")]
    public float BulletSpeed;
    public GameObject GrenadeBullet;
    public Transform ShotBorn;
    
    //计时器
    float timer;
    //射击相关粒子
    ParticleSystem gunParticles;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake()
    {
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            transform.position = ShotBorn.position;
            transform.rotation = ShotBorn.rotation;
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        gunLight.enabled = false;
    }


    public void Shoot()
    {
        if (Ammo <= 0)
            return;
        timer = 0f;
        Ammo--;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        GameObject bullet = Instantiate(GrenadeBullet, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed, ForceMode.Impulse); 

        ani.SetTrigger("Shoot");
        
    }

    public void SetAmmo(int Plus)
    {
        Ammo += Plus;
    }

    public string GetAmmo()
    {
        return Name + ":" + Ammo;
    }
}
