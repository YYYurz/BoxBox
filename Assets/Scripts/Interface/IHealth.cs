using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public interface IHealth
{
    void Death();

    void TakeDamage(int damage);

    void TakeGunShotDamage(int damage);

    int GetHealth();

    void SetHealth(int Health);

}

