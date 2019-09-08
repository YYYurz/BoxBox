using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


interface IShoot
{
    void Shoot();
    string GetAmmo();
    void SetAmmo(int Plus);

}

