using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject Controll;
    IMove Controller;

    float h = 0, v = 0;

    private void Start()
    {
        Controller = Controll.GetComponent<IMove>();
    }

    private void FixedUpdate()
    {
        InputControl();
    }

    void InputControl()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Controller.Move(new Vector3(h, 0, v));

    }

}
