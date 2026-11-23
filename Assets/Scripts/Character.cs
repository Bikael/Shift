
using UnityEngine;

public enum CharacterType
{
    Player,
    Enemy
}

public class Character : MonoBehaviour
{
    public CharacterType currentType;

    private PlayerController playerController;
    private EnemyAI enemyAI;
    private CharacterCombat characterCombat;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        enemyAI = GetComponent<EnemyAI>();
        characterCombat = GetComponent<CharacterCombat>();
    }

    private void Start()
    {
        UpdateComponents();
    }

    public void BecomePlayer()
    {
        currentType = CharacterType.Player;
        UpdateComponents();
    }

    public void BecomeEnemy()
    {
        currentType = CharacterType.Enemy;
        UpdateComponents();
    }

    private void UpdateComponents()
    {
        bool isPlayer = currentType == CharacterType.Player;

        if (playerController != null)
            playerController.enabled = isPlayer;

        if (enemyAI != null)
            enemyAI.enabled = !isPlayer;

        if (characterCombat != null)
            characterCombat.enabled = isPlayer;
    }
}
