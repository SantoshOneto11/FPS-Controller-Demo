using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController _characterController;
    private float _movementSpeed = 10f;
    private float _mouseSensitivity = 100f;
    private float _verticalLookRotation = 0f; // To control vertical (up/down) rotation

    public WeaponController weaponController;
    int defaultGunIndex = 0;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        weaponController.EquiqWeapon(defaultGunIndex);

        // Lock cursor in the center and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            weaponController.EquiqWeapon(defaultGunIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponController.ActiveWeaponIndex = 1;
            weaponController.EquiqWeapon(weaponController.ActiveWeaponIndex);
        }
    }

    private void HandleMovement()
    {
        // Get input for movement
        float XMove = Input.GetAxis(StaticValues.Horizontal);
        float ZMove = Input.GetAxis(StaticValues.Vertical);

        Vector3 movement = transform.right * XMove + transform.forward * ZMove;
        movement *= _movementSpeed * Time.deltaTime;

        // Move the player
        _characterController.Move(movement);
    }

    private void HandleMouseLook()
    {
        // Get mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        // Rotate the player horizontally (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Control vertical look (pitch) by clamping vertical rotation
        _verticalLookRotation -= mouseY;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90f, 90f); // Prevents looking too far up or down

        // Apply the vertical rotation to the camera or player head (if the camera is a child of the player)
        Camera.main.transform.localRotation = Quaternion.Euler(_verticalLookRotation, 0f, 0f);
    }
}
