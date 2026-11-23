
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        // Only move horizontally
        direction.y = 0f;

        // Prevent diagonal movement from being faster
        direction.Normalize();

        // Apply movement velocity
        rb.linearVelocity = new Vector3(
            direction.x * movementSpeed,
            rb.linearVelocity.y,
            direction.z * movementSpeed
        );
    }

    public void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }
}
