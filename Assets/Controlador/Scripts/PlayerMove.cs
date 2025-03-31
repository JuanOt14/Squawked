using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float Speed = 3.0f;
    private float RotationSpeed = 80.0f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(vertical * -1, 0.0f, 0.0f) * Time.deltaTime * Speed);
        transform.Rotate(new Vector3(0, horizontal * Time.deltaTime * RotationSpeed, 0));
    }
}
