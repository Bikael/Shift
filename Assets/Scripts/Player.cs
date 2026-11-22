using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    [Header("Projectile")]
    public float bulletSpeed = 5f;
    public Transform bulletLaunchPoint;
    public float launchOffset = 1f;
    [SerializeField] private GameObject bullet;

    [Header("Movement")]
    public float movementSpeed = 5f;
    float xMovementInput;
    float zMovementInput;
    private Rigidbody rb; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Shoot();
    }
    void FixedUpdate()
    {
        PlayerMovement();
        
    }

    void Shoot()
    {
        // Get mouse position
        Vector3 mousePos = Mouse.current.position.ReadValue();

        // Create ray from camera through mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        // Find intersection with ground plane
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);

            // Calculate direction from player to mouse
            Vector3 direction = (targetPoint - transform.position);
            direction.y = 0f;
            direction.Normalize();

            // Position launch point around player
            bulletLaunchPoint.localPosition = new Vector3(
                direction.x * launchOffset,
                bulletLaunchPoint.localPosition.y,
                direction.z * launchOffset
            );

            // Shoot
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                GameObject projectile = Instantiate(
                    bullet,
                    bulletLaunchPoint.position,
                    Quaternion.identity
                );

                Rigidbody bulletRB = projectile.GetComponent<Rigidbody>();

                bulletRB.linearVelocity = direction * bulletSpeed;
            }
        }
    }

    private void OnDrawGizmos()
{
    if (bulletLaunchPoint != null)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(bulletLaunchPoint.position, 0.1f);
    }
}

    void PlayerMovement()
    {
        xMovementInput = 0f;
        zMovementInput = 0f;

        if (Keyboard.current.dKey.isPressed)
        {
            xMovementInput = 1f;
        } if (Keyboard.current.aKey.isPressed)
        {
            xMovementInput = -1f;
        } if (Keyboard.current.wKey.isPressed)
        {
            zMovementInput = 1f;
        } if (Keyboard.current.sKey.isPressed)
        {
            zMovementInput = -1f;
        }

        Vector3 movementDirection = new Vector3(
            xMovementInput,
            0f,
            zMovementInput
        ).normalized;

        rb.linearVelocity = new Vector3(
            movementDirection.x * movementSpeed, 
            rb.linearVelocity.y, 
            movementDirection.z * movementSpeed
            );
    }
}

