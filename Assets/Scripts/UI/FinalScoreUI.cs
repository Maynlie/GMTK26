using TMPro;
using UnityEngine;

public class FinalScoreUI : MonoBehaviour
{
    public TextMeshProUGUI finalScorTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalScorTxt.text = "Game Over\r\nFinal Score : " + GameManager.Instance.GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
