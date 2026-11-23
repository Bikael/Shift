
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Character")]
    [SerializeField] private Character currentPlayer;
    [SerializeField] private FollowPlayer followPlayer;
    public Character CurrentPlayer => currentPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (currentPlayer != null)
        {
            currentPlayer.BecomePlayer();
            UpdateEnemyTargets();
            currentPlayer.GetComponent<CharacterMovement>().SetMovementSpeed(15f);
        }
    }

    private void Update()
{
    if (Keyboard.current == null)
        return;

    if (Keyboard.current.pKey.wasPressedThisFrame)
    {
        Debug.Log("P key pressed!");

        EnemyAI[] enemies = FindObjectsByType<EnemyAI>(
            FindObjectsInactive.Exclude
        );

        Debug.Log("EnemyAI components found: " + enemies.Length);

        foreach (EnemyAI enemy in enemies)
        {
            Character character = enemy.GetComponent<Character>();

            if (character != null &&
                character.currentType == CharacterType.Enemy)
            {
                Debug.Log("Switching to: " + character.name);
                SwapCharacters(character);
                break;
            }
        }
    }
}

    public void SwapCharacters(Character newPlayer)
    {
        if (newPlayer == null || newPlayer == currentPlayer)
            return;

        // Store the character we were previously controlling
        Character previousPlayer = currentPlayer;

        // Change the previous player into an enemy
        previousPlayer.BecomeEnemy();

        // Change the new character into the player
        newPlayer.BecomePlayer();

        // Update the currently controlled character
        currentPlayer = newPlayer;

        // Update camera to follow the new player
        followPlayer.SetTarget(currentPlayer.transform);
        
        // Redirect all enemies toward the new player
        UpdateEnemyTargets();

        // Set the new player's movement speed
        newPlayer.GetComponent<CharacterMovement>().SetMovementSpeed(15f);

        // Set the previous player's movement speed
        previousPlayer.GetComponent<CharacterMovement>().SetMovementSpeed(5f);
    }

    private void UpdateEnemyTargets()
    {
        EnemyAI[] enemies = FindObjectsByType<EnemyAI>(
            FindObjectsInactive.Exclude
        );

        foreach (EnemyAI enemy in enemies)
        {
            if (enemy.enabled)
            {
                enemy.SetTarget(currentPlayer.transform);
            }
        }
    }
}
