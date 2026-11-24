
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();

        if (character != null)
        {
            if (character.currentType == CharacterType.Enemy)
                {
                    TimeManager.Instance.TriggerSlowMotion();

                    GameManager.Instance.SwapCharacters(character);
                }

            Destroy(gameObject);
        }
    }
}
