using UnityEngine;
using CurvedPathGenerator;

public class LutinBehavior : MonoBehaviour
{
    public enum LutinState
    {
        WaitingForOrder,
        GoingToShelves,
        SearchingGift,
        GoingBackstage,
        WaitingInLine
    }
    [SerializeField]LutinState currentState;
    Vector2Int giftPosition = new Vector2Int(0,0);
    public PathFollower follow;
    Vector3 deskPos, shelvePos, startLine, middleLine, startRushPos, endRudhPos;
    float waitingTime = 0f;
    public float startSpeed;
    float distancehreshold = 0.5f;
    int stepInLine = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deskPos = follow.Generator.NodeList_World[0];
        shelvePos = follow.Generator.NodeList_World[1];
        startLine = follow.Generator.NodeList_World[5];
        middleLine = follow.Generator.NodeList_World[6];
        startRushPos = follow.Generator.NodeList_World[2];
        endRudhPos = follow.Generator.NodeList_World[4];

        follow.Speed = startSpeed;
    }

    public LutinState GetCurrentState()
    {
        return currentState;
    }

    public void ReceiveOrder(Vector2Int order)
    {
        giftPosition = order;
        currentState = LutinState.GoingToShelves; 
        follow.IsMove = true;
    }

    void ShelvesReached()
    {
        currentState = LutinState.SearchingGift;
        waitingTime = 2.0f;
        follow.IsMove = false;
    }

    void GiftFound()
    {
        currentState = LutinState.GoingBackstage;
        follow.IsMove = true;
    }

    void OtherImpDetected()
    {
        currentState = LutinState.WaitingInLine;
        waitingTime = 2.0f;
        follow.IsMove = false;
        stepInLine++;
    }

    void DeskDetected()
    {
        currentState = LutinState.WaitingForOrder;
        follow.IsMove = false;
        stepInLine = 0;
    }

    

    // Update is called once per frame
    void Update()
    {
        switch(currentState) {
            case LutinState.GoingToShelves:
                // Handle going to shelves logic
                
                if(Vector3.Distance(transform.position, shelvePos) <= distancehreshold)
                {
                    ShelvesReached();
                }
                break;
            case LutinState.SearchingGift:
                // Handle searching for gift logic
                if(waitingTime > 0)
                {
                    waitingTime -= Time.deltaTime;
                }
                else
                {
                    GiftFound();
                }
                break;
            case LutinState.GoingBackstage:
                // Handle going backstage logic
                if (Vector3.Distance(transform.position, startRushPos) <= distancehreshold)
                {
                    follow.Speed = 600.0f; // Increase speed for rush
                }
                else if (Vector3.Distance(transform.position, endRudhPos) <= distancehreshold)
                {
                    follow.Speed = startSpeed; // Reset speed after rush
                }
                else if (Vector3.Distance(transform.position, startLine) <= distancehreshold)
                {
                    OtherImpDetected();
                }
                break;
            case LutinState.WaitingInLine:
                // Handle waiting in line logic
                if (waitingTime > 0)
                {
                    waitingTime -= Time.deltaTime;
                }
                else
                {
                    if(stepInLine == 1)
                    {
                        if (Vector3.Distance(transform.position, middleLine) >= distancehreshold && !follow.IsMove)
                        {
                            follow.IsMove = true; // Resume moving after waiting
                        }
                        else if (Vector3.Distance(transform.position, middleLine) <= distancehreshold)
                        {
                            OtherImpDetected();
                        }
                    }
                    else if (stepInLine == 2)
                    {
                        if (Vector3.Distance(transform.position, deskPos) >= distancehreshold && !follow.IsMove)
                        {
                            follow.IsMove = true; // Resume moving after waiting
                        }
                        else if (Vector3.Distance(transform.position, deskPos) <= distancehreshold)
                        {
                            DeskDetected();
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
}
