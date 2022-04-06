using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionsHandler : MonoBehaviour
{
    LevelHandler levelHandler;
    BoxCollider2D playerCollider;
    MovementScript movementScript;

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        movementScript = GameObject.Find("Player").GetComponent<MovementScript>();
        levelHandler = GameObject.Find("Game session").GetComponent<LevelHandler>();
    }

    void OnInteractWithObject(InputValue value)
    {
        LayerMask mask = LayerMask.GetMask("Door");
        LayerMask maskSecretPassage = LayerMask.GetMask("SecretPassage");
        if(value.isPressed && playerCollider.IsTouchingLayers(mask))
        {
            levelHandler.QuitUsingDoor();
        } else if(value.isPressed && playerCollider.IsTouchingLayers(maskSecretPassage))
        {
            movementScript.MoveToNearestSecretDoor();
        }
    }
}
