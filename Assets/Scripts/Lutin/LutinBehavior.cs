using UnityEngine;

public class LutinBehavior : MonoBehaviour
{
    private enum LutinState
    {
        WaitingForOrder,
        GoingToShelves,
        SearchingGift,
        GoingBackstage,
        WaitingInLine
    }
    LutinState currentState = LutinState.WaitingForOrder;
    Vector2Int giftPosition = new Vector2Int(0,0);  
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void ReceiveOrder(Vector2Int order)
    {
        giftPosition = order;
        currentState = LutinState.GoingToShelves;
    }

    void ShelvesReached()
    {
        currentState = LutinState.SearchingGift;
    }

    void GiftFound()
    {
        currentState = LutinState.GoingBackstage;
    }

    void OtherImpDetected()
    {
        currentState = LutinState.WaitingInLine;
    }

    void DeskDetected()
    {
        currentState = LutinState.WaitingForOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
