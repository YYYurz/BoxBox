using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//灯光闪烁
public class LightFlicker : MonoBehaviour
{
    public float FlickerTime = 1f;

    Light lampLight;
    float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        lampLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= FlickerTime)
        {
            lampLight.intensity = 6f;
            timer = 0f;
        }
        if(timer >= FlickerTime * 0.1f)
        {
            lampLight.intensity = 0f;
        }
    }
    
    
}
