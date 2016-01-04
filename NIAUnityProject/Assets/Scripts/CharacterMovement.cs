using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float turnSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;

    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Vector3 zeroVector = transform.TransformDirection(new Vector3(0, 0, Input.GetAxis("Vertical")));
        if (controller.isGrounded)
        {
            moveDirection = zeroVector;
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * turnSpeed, 0));

    }
}