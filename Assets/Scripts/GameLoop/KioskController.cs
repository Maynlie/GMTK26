using UnityEngine;
using UnityEngine.InputSystem;

public class KioskController : MonoBehaviour
{
    enum Position
    {
        Left,
        Center,
        Right
    }

    private Position currentPosition = Position.Center;
    public InputActionReference MoveDirectionAction;
    public Camera Camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveDirectionAction.action.performed += MovePOV;
    }

    private void MovePOV(InputAction.CallbackContext context)
    {
        float moveValue = context.ReadValue<float>();
        if (moveValue > 0)
        {
            // Move to the right
            if(currentPosition < Position.Right)
            {
                currentPosition++;
                Camera.transform.Rotate(new Vector2(0, 90));
            }
        }
        else if(moveValue < 0)
        {
            // Move to the left
            if (currentPosition > Position.Left)
            {
                currentPosition--;
                Camera.transform.Rotate(new Vector2(0, -90));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
