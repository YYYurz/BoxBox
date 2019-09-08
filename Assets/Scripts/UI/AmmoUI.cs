using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public GameObject Gun;
    Text Ammo;

    // Start is called before the first frame update
    void Start()
    {
        Ammo = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Ammo.text = Gun.GetComponent<IShoot>().GetAmmo();
    }
}
