
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private Transform player;

    private void Start()
    {
        SetTarget(GameManager.Instance.CurrentPlayer.transform);
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        transform.position = player.position + offset;
    }

    public void SetTarget(Transform newTarget)
    {
        player = newTarget;
    }
}
