using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Camera.main.transform.position - transform.position;
        Vector3 n = dir;
        n.x = 0;
        transform.rotation = Quaternion.LookRotation(n);
    }
}
