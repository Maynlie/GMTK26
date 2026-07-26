using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

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
    public InputActionReference GetInfoAction;
    public InputActionReference GrabLetterAction;
    public Camera Camera;
    public LutinBehavior[] lutins;
    public GameObject ImpInfos;

    public AudioSource source;
    public AudioClip[] think;
    public AudioClip[] ohoh;

    float thinkTimer = 0f;
    float thinkCooldown = 45f;

    public LayerMask rayLayer;
    public LetterTable table;
    int letterIndex;

    public GameObject letterUIAnchor;
    public GameObject letterUIPrefab;

    bool letterOpenned = false;

    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI helperTxt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveDirectionAction.action.performed += MovePOV;
        GetInfoAction.action.performed += GetInfo;
        GrabLetterAction.action.performed += GrabLetter;

        GameManager.Instance.SetLutins(lutins, this);
    }

    private void OnDisable()
    {
        MoveDirectionAction.action.performed -= MovePOV;
        GetInfoAction.action.performed -= GetInfo;
        GrabLetterAction.action.performed -= GrabLetter;
    }

    private void MovePOV(InputAction.CallbackContext context)
    {
        if(Time.timeScale == 0) return; // Do not move if the game is paused
        if (ImpInfos.activeSelf) return;
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
        UpdateHelperText();
    }

    public void CloseLetter()
    {
        source.clip = ohoh[Random.Range(0, ohoh.Length)];
        source.Play();
        GameObject.Destroy(letterUIAnchor.transform.GetChild(0).gameObject);
        letterOpenned = false;
    }

    public void UpdateScoreUI()
    {
        scoreTxt.text = "Score: " + GameManager.Instance.GetScore();
    }

    public void UpdateHelperText()
    {
        switch (currentPosition)
        {
            case Position.Left:
                helperTxt.text = "L: Grab a letter";
                break;
            case Position.Center:
                helperTxt.text = "I : Open/Close Imp Info";
                break;
            case Position.Right:
                helperTxt.text = "";
                break;
        }
    }

    private void GetInfo(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0) return;
        if (currentPosition != Position.Center) return;
        ImpInfos.SetActive(!ImpInfos.activeSelf);
    }

    private void GrabLetter(InputAction.CallbackContext context)
    {
        if(letterOpenned) return;
        if (Time.timeScale == 0) return;
        if (currentPosition != Position.Left) return;
        table.RemoveLetter(letterIndex);
        letterIndex++;
        if (letterIndex >= 8)
        {
            letterIndex = 0;
        }

        Instantiate(letterUIPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero), letterUIAnchor.transform).transform.localPosition = Vector3.zero;
        letterOpenned = true;
    }

    // Update is called once per frame
    void Update()
    {
        thinkTimer += Time.deltaTime;
        if(thinkTimer > thinkCooldown)
        {
            source.clip = think[Random.Range(0, think.Length)];
            if(!source.isPlaying)
            {
                source.Play();
            }
            thinkTimer = 0f;
        }

        
    }
}
