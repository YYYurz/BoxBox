using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    [Header("爆炸")]
    public ParticleSystem Explosive;
    [Header("榴弹消失时间")]
    public float despawnTime = 1f;
    [Header("伤害")]
    public int Damage = 20;
    public MeshRenderer mesh;

    private AudioSource au;
    private float radiu = 5f;
    private bool isBoom = false;
    private Collider[] colliders;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isBoom)
        {
            colliders = Physics.OverlapSphere(transform.position, radiu);

            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Target")
                {
                    collider.GetComponent<IHealth>().TakeDamage(Damage / 2);
                    collider.GetComponent<Rigidbody>().AddExplosionForce(1000f, transform.position, radiu);
                }
            }
            mesh.enabled = false;
            Explosive.Play();
            au.Play();
        }
        isBoom = true;
        Destroy(gameObject, despawnTime);
    }


    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
    }
    
}
