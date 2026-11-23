
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCombat : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletLaunchPoint;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float launchOffset = 0.7f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Aim();
        Shoot();
    }

    private void Aim()
    {
        if (Mouse.current == null || mainCamera == null)
            return;

        Vector3 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);

            Vector3 direction = targetPoint - transform.position;
            direction.y = 0f;
            direction.Normalize();

            bulletLaunchPoint.localPosition = new Vector3(
                direction.x * launchOffset,
                bulletLaunchPoint.localPosition.y,
                direction.z * launchOffset
            );
        }
    }

    private void Shoot()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject projectile = Instantiate(
                bullet,
                bulletLaunchPoint.position,
                Quaternion.identity
            );

            Vector3 direction = (
                bulletLaunchPoint.position - transform.position
            ).normalized;

            direction.y = 0f;
            direction.Normalize();

            Rigidbody bulletRB = projectile.GetComponent<Rigidbody>();

            bulletRB.linearVelocity = direction * bulletSpeed;
        }
    }
}
