using UnityEngine;
using CurvedPathGenerator;
using Unity.VisualScripting;

public class LutinBehavior : MonoBehaviour
{
    public enum LutinState
    {
        WaitingForOrder,
        GoingToShelves,
        SearchingGift,
        GoingBackstage,
        WaitingInLine,
        InitGoToDesk,
        Idle
    }

    public enum LutinType
    {
        Bob,
        Giselle,
        Didier
    }
    [SerializeField]LutinState currentState;
    LutinState previousState;
    Vector2Int giftPosition = new Vector2Int(0,0);
    public PathFollower follow;
    PathGenerator path;
    Vector3 deskPos, shelvePos, startLine, middleLine, startRushPos, endRudhPos;
    float waitingTime = 0f;
    public float startSpeed;
    float distancehreshold = 0.5f;
    float distaMinImp = 1.5f;
    int stepInLine = 0;
    public LutinType lutinType;
    public LutinBehavior previousImp;
    bool initEnded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (lutinType == LutinType.Bob)
        {
            path = follow.Generator;
            deskPos = follow.Generator.NodeList_World[0];
            shelvePos = follow.Generator.NodeList_World[1];
            startLine = follow.Generator.NodeList_World[5];
            middleLine = follow.Generator.NodeList_World[6];
            startRushPos = follow.Generator.NodeList_World[2];
            endRudhPos = follow.Generator.NodeList_World[4];

            follow.Speed = startSpeed;

            previousImp.updateNodes(deskPos, shelvePos, startLine, middleLine, startRushPos, endRudhPos, path);
            previousImp.previousImp.updateNodes(deskPos, shelvePos, startLine, middleLine, startRushPos, endRudhPos, path);
            initEnded = true;
        }
    }

    public void updateNodes(Vector3 desk, Vector3 shelve, Vector3 startLinePos, Vector3 middleLinePos, Vector3 startRush, Vector3 endRush, PathGenerator p)
    {
        deskPos = desk;
        shelvePos = shelve;
        startLine = startLinePos;
        middleLine = middleLinePos;
        startRushPos = startRush;
        endRudhPos = endRush;
        path = p;
    }

    public void InitGoToDesk()
    {
        currentState = LutinState.InitGoToDesk;
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
        if(!previousImp.initEnded)
        {
            previousImp.InitGoToDesk();
        }
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
        if(currentState != LutinState.WaitingInLine)
        {
            previousState = currentState;
            currentState = LutinState.WaitingInLine;
        }
       
        follow.IsMove = false;
    }

    void DeskDetected()
    {
        if (!initEnded)
        {
            initEnded = true;
            follow.Generator = path;
        }
        currentState = LutinState.WaitingForOrder;
        follow.IsMove = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if(initEnded && Vector3.Distance(transform.position, previousImp.previousImp.transform.position) <= distaMinImp && Vector3.Angle(transform.forward, previousImp.previousImp.transform.position - transform.position) <= 10f)
        {
            OtherImpDetected();
        }
        switch (currentState) {
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
                else if (Vector3.Distance(transform.position, deskPos) <= distancehreshold)
                {
                    DeskDetected();
                }
                break;
            case LutinState.WaitingInLine:
                // Handle waiting in line logic
                if (Vector3.Distance(transform.position, previousImp.previousImp.transform.position) >= distaMinImp || Vector3.Angle(transform.forward, previousImp.previousImp.transform.position - transform.position) >= 10f)
                {
                    follow.IsMove = true;
                    currentState = previousState;
                }
                break;
            case LutinState.InitGoToDesk:
                Vector3 direction = (deskPos - transform.position).normalized;
                transform.position += direction * startSpeed * Time.deltaTime;
                if(Vector3.Distance(transform.position, deskPos) <= distancehreshold)
                {
                    DeskDetected();
                }
                break;
            default:
                break;
        }
    }
}
