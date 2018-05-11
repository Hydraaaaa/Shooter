using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    public CharacterController Controller { get { return controller; } }

    [SerializeField] Player player;
    [SerializeField] CharacterController controller;

    [SerializeField]
    float speed = 1;

    void Update()
    {
        if (player.Identity.hasAuthority)
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                direction += transform.forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                direction -= transform.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                direction -= transform.right;
            }

            if (Input.GetKey(KeyCode.D))
            {
                direction += transform.right;
            }

            direction.y = 0;

            controller.Move(direction.normalized * speed * Time.deltaTime);
        }

        if (!controller.isGrounded)
        {
            controller.Move(new Vector3(0, -1, 0));
        }
    }
}
