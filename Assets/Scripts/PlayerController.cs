
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    private CharacterMovement movement;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float xInput = 0f;
        float zInput = 0f;

        if (Keyboard.current.dKey.isPressed)
            xInput = 1f;

        if (Keyboard.current.aKey.isPressed)
            xInput = -1f;

        if (Keyboard.current.wKey.isPressed)
            zInput = 1f;

        if (Keyboard.current.sKey.isPressed)
            zInput = -1f;

        Vector3 movementDirection = new Vector3(
            xInput,
            0f,
            zInput
        );

        movement.Move(movementDirection);
    }
}
