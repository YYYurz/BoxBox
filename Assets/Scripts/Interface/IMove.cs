using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public interface IMove
{
    void Move(Vector3 vector);

    void Move();

    void PlayStepSound();

    void WalkAnim();
}

