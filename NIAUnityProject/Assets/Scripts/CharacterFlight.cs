using UnityEngine;
using System.Collections;

public class CharacterFlight : MonoBehaviour
{
    public float speed = 6.0f;
    public float upSpeed = 10.0f;
    public float gravity = 20.0f;
    public float glide = 0.1f;
    private Vector3 moveDirection = Vector3.zero;

    CharacterController controller;
    private AudioSource audioSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Vector3 moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        moveDirection.x = moveDir.x * speed;
        moveDirection.z = moveDir.z * speed;

        if (Input.GetButtonDown("Flight Up"))
        {
            moveDirection.y = upSpeed;
            audioSource.PlayOneShot(audioSource.clip);
        }
        

        if (Input.GetButton("Glide") && moveDirection.y < 0)
        {
            moveDirection.y -= gravity * glide * Time.deltaTime;
        } else
            moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
