using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveShark : MonoBehaviour
{
    //for easier adjustment of character speed in the editor
    [Range(0f, 7f)]
    public float speed = 3f;

    //for easier adjustment of shark turn speed in the editor
    [Range(10f, 30f)]
    public float turnSpeed = 7f;

    public Vector2 k_inputs;

    CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //y takes in the horizontal because to turn on the horizontal plane you need to turn on the y axis
        //x takes in the vertical becasue to turn up and down you need to turn on the x axis
        k_inputs.y += Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        k_inputs.x += Input.GetAxis("Vertical") * turnSpeed * Time.deltaTime;

        if (!GameManager.manager.endGame)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        controller.Move(speed * transform.forward * Time.deltaTime);
    }

    void Rotate()
    {
        Vector3 eulerRotation;

        k_inputs.x = Mathf.Clamp(k_inputs.x, -80f, 80f);

        eulerRotation.x = k_inputs.x;
        eulerRotation.y = k_inputs.y;

        //extra safety to keep the shark from doing barrel rolls anywhere else
        eulerRotation.z = 0f;

        transform.eulerAngles = eulerRotation;
    }

    public void ReturnToZero()
    {
        controller.enabled = false;
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        controller.enabled = true;
    }
}
