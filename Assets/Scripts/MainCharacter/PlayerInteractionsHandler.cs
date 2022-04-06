using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionsHandler : MonoBehaviour
{
    LevelHandler levelHandler;
    BoxCollider2D playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        levelHandler = GameObject.Find("Game session").GetComponent<LevelHandler>();
    }

    void OnInteractWithObject(InputValue value)
    {
        LayerMask mask = LayerMask.GetMask("Door");
        if(value.isPressed && playerCollider.IsTouchingLayers(mask))
        {
            levelHandler.QuitUsingDoor();
        }
    }
}
