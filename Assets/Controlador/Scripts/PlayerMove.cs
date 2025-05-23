using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float Speed = 3.0f;
    private float RotationSpeed = 80.0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movimiento hacia adelante y atrás
        transform.Translate(transform.forward * vertical * Time.deltaTime * Speed, Space.World);

        // Rotación lateral
        if (Mathf.Abs(horizontal) > 0.01f)
        {
            transform.Rotate(Vector3.up * horizontal * Time.deltaTime * RotationSpeed);
        }

        // Animaciones
        animator.SetBool("isSwimming", vertical > 0.01f); // caminar hacia adelante
        animator.SetBool("isWalkingBack", vertical < -0.01f); // caminar hacia atrás
    }
}