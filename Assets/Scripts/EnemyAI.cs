
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    private CharacterMovement movement;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void FixedUpdate()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if (target == null) return;

        // Calculate direction toward target
        Vector3 direction = target.position - transform.position;

        // Only move horizontally
        direction.y = 0f;
        // Tell CharacterMovement to move
        movement.Move(direction);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
