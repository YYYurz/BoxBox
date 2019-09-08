using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//火光颤动
public class LightShake : MonoBehaviour
{
    Vector3 StartPos;
    Vector3 randomPos;

    public float minIntensity = 0.25f;
    public float maxIntensity = 0.5f;

    float random;
    float TimeSinceRandomRefresh = 9999.0f;

    void Start()
    {
        StartPos = transform.position;
        random = Random.Range(0.0f, 25000.0f);
    }

    void Update()
    {
        setRandomPos(0.15f);
        RandomLerpPos(0.55f);


        float noise = Mathf.PerlinNoise(random, Time.time);
        GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }

    void RandomLerpPos(float speed)
    {
        Vector3 newPos = Vector3.Lerp(transform.position, randomPos, Time.deltaTime * speed);
        transform.position = newPos;
    }

    void setRandomPos(float interval)
    {
        if (TimeSinceRandomRefresh > interval)
        {
            randomPos = Random.insideUnitSphere;
            randomPos += StartPos;

            TimeSinceRandomRefresh = 0.0f;
        }
        else
        {
            TimeSinceRandomRefresh += Time.deltaTime;
        }
    }
}
