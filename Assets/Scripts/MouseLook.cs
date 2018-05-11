using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform HorizontalRotator { get { return horizontalRotator; } }
    public Transform VerticalRotator { get { return verticalRotator; } }

    [SerializeField] Player player;

    [SerializeField] Transform horizontalRotator;
    [SerializeField] Transform verticalRotator;

    [SerializeField] float sensitivity = 1;

    void Start()
    {
    }

    void Update()
    {
        if (player.Identity.hasAuthority)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                horizontalRotator.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0);

                float mouseY = Input.GetAxis("Mouse Y");

                if (verticalRotator.localEulerAngles.x - mouseY * sensitivity > 90 &&
                    verticalRotator.localEulerAngles.x - mouseY * sensitivity < 180)
                {
                    verticalRotator.localEulerAngles = new Vector3(90, 0, 0);
                    return;
                }

                if (verticalRotator.localEulerAngles.x - mouseY * sensitivity < 270 &&
                    verticalRotator.localEulerAngles.x - mouseY * sensitivity >= 180)
                {
                    verticalRotator.localEulerAngles = new Vector3(270, 0, 0);
                    return;
                }

                verticalRotator.localEulerAngles += new Vector3(-mouseY * sensitivity, 0, 0);

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
