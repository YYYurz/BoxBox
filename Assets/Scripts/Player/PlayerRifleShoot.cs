using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRifleShoot : MonoBehaviour,IShoot
{
    [Header("名称")]
    public string Name;
    [Header("子弹数量")]
    public int Ammo = 0;
    [Header("每枪伤害")]
    public int damagePerShot = 20;
    [Header("每枪间隔")]
    public float timeBetweenBullets = 0.15f;
    [Header("射击距离")]
    public float range = 100f;
    [Header("动画")]
    public Animator ani;
    public Transform ShotBorn;
    

    //计时器
    float timer;
    //射击 射线 粒子 线等
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.1f;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
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

    //射击效果关闭
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    //射击效果
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

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        ani.SetTrigger("Shoot");

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            IHealth enemyHealth = shootHit.collider.GetComponent<IHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
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
