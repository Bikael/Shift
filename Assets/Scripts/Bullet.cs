using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 3f;

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }
}
