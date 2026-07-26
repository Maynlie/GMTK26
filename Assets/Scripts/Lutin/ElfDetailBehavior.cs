using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ElfDetailBehavior : MonoBehaviour
{
    public LutinBehavior.LutinType type;

    public RawImage impImage;
    public TextMeshProUGUI impNameText;
    public TextMeshProUGUI impDescriptionText;
    public TextMeshProUGUI impEffectText;
    public InputActionReference MoveDirectionAction;

    public AudioSource audioSource;
    public AudioClip open;
    public AudioClip move;

    [SerializeField] Texture2D[] impTextures;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        DisplayImpInfo();
        MoveDirectionAction.action.performed += MoveInfos;
    }

    private void OnEnable()
    {
        audioSource.clip = open;
        audioSource.Play();
    }

    private void MoveInfos(InputAction.CallbackContext context)
    {
        float moveValue = context.ReadValue<float>();
        if (moveValue > 0f)
        {
            NextImp();
        }
        else if(moveValue < 0f)
        {
            PreviousImp();
        }
    }

    public void NextImp()
    {
        audioSource.clip = move;
        audioSource.Play();
        if (type == LutinBehavior.LutinType.Didier)
        {
            type = LutinBehavior.LutinType.Bob;
        }
        else
        {
            type++;
        }
        DisplayImpInfo();
    }

    public void PreviousImp()
    {
        audioSource.clip = move;
        audioSource.Play();
        if (type == LutinBehavior.LutinType.Bob)
        {
            type = LutinBehavior.LutinType.Didier;
        }
        else
        {
            type--;
        }
        DisplayImpInfo();
    }

    public void CloseInfo()
    {
        gameObject.SetActive(false);
    }

    private void DisplayImpInfo()
    {
        switch (type)
        {
            case LutinBehavior.LutinType.Bob:
                impImage.texture = impTextures[0];
                impNameText.text = "Bob";
                impDescriptionText.text = "Bob is a really clumsy elf";
                impEffectText.text = "Effect: When picking an item on the shelf, all the others items of the same row will fall. The new items are randomly replaced.";
                break;
            case LutinBehavior.LutinType.Giselle:
                impImage.texture = impTextures[1];
                impNameText.text = "Giselle";
                impDescriptionText.text = "Giselle has a poor eyesight";
                impEffectText.text = "Effect: When a cell on the shelf is targeted, she will always take the item right of it.";
                break;
            case LutinBehavior.LutinType.Didier:
                impImage.texture = impTextures[2];
                impNameText.text = "Didier";
                impDescriptionText.text = "Didier has an alcohol issue";
                impEffectText.text = "Effect: When picking a gift on the shelf, all the other gifts are randomly reshuffled.";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
