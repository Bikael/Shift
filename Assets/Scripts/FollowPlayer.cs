using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 5f;

    private Transform player;

    private void Start()
    {
        SetTarget(GameManager.Instance.CurrentPlayer.transform);
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        player = newTarget;
    }
}
