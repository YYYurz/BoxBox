using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public float smooth;

    private Vector3 offset;

    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, Pos, smooth * Time.deltaTime);
    }
}
