using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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
    public InputActionReference GiveOrderAction;
    public Camera Camera;
    public LutinBehavior[] lutins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveDirectionAction.action.performed += MovePOV;
        GiveOrderAction.action.performed += GiveOrder;
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

    private void GiveOrder(InputAction.CallbackContext context)
    {
        foreach (LutinBehavior lut in lutins)
        {
            if(lut.GetCurrentState() == LutinBehavior.LutinState.WaitingForOrder)
            {
                lut.ReceiveOrder(new Vector2Int(1, 1)); // Example order, replace with actual logic
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
