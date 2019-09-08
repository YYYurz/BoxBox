using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotGun : MonoBehaviour,IShoot
{
    [Header("名称")]
    public string Name;
    [Header("子弹数量")]
    public int Ammo = 0;
    [Header("每枪伤害")]
    public int damagePerShot = 20;
    [Header("每枪间隔")]
    public float timeBetweenBullets = 0.15f;
    [Header("动画")]
    public Animator ani;
    public Transform ShotBorn;
    
    //计时器
    float timer;

    //射击粒子相关
    int shootableMask;
    ParticleSystem gunParticles;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.GetComponent<IHealth>() != null)
        {
            other.gameObject.GetComponent<IHealth>().TakeGunShotDamage(damagePerShot);
        }
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

        ani.SetTrigger("Shoot");
    }

    public void SetAmmo(int Plus)
    {
        Ammo += Plus;
    }

    public string GetAmmo()
    {
        return Name + ":" +  Ammo;
    }

}
