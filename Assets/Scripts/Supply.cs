using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//补给箱
public class Supply : MonoBehaviour
{
    [Header("动画")]
    public Animator anim;
    [Header("补给箱文本")]
    public Text SupplyText;

    IShoot[] GunSupply;
    MeshRenderer mesh;

    ObjectPool Pool;

    //补给箱中每种补给的概率
    int[] Chance = { 0, 0, 0, 0, 0, 0, 0 , 1, 1, 1, 2, 2, 3, 3 ,3,3,3 };
    int SupplyType = 0;

    int SupplyNum = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Pool = ObjectPool.GetInstance();
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = true;
        SupplyType = Chance[Random.Range(0, Chance.Length - 1)];
    }

    private void OnDisable()
    {
        SupplyText.color = new Color(1,1,1,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Player"))
        {
            //按照概率生成相应的补给箱
            GunSupply = GameObject.Find("ShotEffect").GetComponentsInChildren<IShoot>(true);
            switch (SupplyType)
            {
                case 0:
                    SupplyNum = Random.Range(80,150);
                    GunSupply[0].SetAmmo(SupplyNum);
                    SupplyText.text = "Rifle +" + SupplyNum;
                    break;
                case 1:
                    SupplyNum = Random.Range(10, 20);
                    GunSupply[1].SetAmmo(SupplyNum);
                    SupplyText.text = "ShotGun +" + SupplyNum;
                    break;
                case 2:
                    SupplyNum = Random.Range(10, 20);
                    GunSupply[2].SetAmmo(SupplyNum);
                    SupplyText.text = "Grenade +" + SupplyNum;
                    break;
                case 3:
                    SupplyNum = Random.Range(10, 25);
                    other.gameObject.GetComponent<IHealth>().SetHealth(SupplyNum);
                    SupplyText.text = "Health +" + SupplyNum;
                    break;
                default:
                    break;
            }


            anim.SetTrigger("Supply");
            mesh.enabled = false;
            StartCoroutine(Recovery());
        }
    }

    IEnumerator Recovery()
    {
        yield return new WaitForSeconds(0.6f);
        Pool.RecycleObj(gameObject);
    }
}

