using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class GameLoopUIManager : MonoBehaviour
{
    public InputActionReference pauseActionRef;
    bool isPaused = false, justChanged = false;
    public GameObject pauseLayer, gameLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Resume()
    {
        isPaused = false;
        justChanged = true;
        pauseLayer.SetActive(false);
        gameLayer.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseActionRef.action.ReadValue<float>() > 0.0f && !justChanged)
        {
            if (!isPaused)
            {
                isPaused = true;
                justChanged = true;
                pauseLayer.SetActive(true);
                gameLayer.SetActive(false);
            }
            else 
            {
                Resume();
            }
        }
        if(pauseActionRef.action.ReadValue<float>() == 0.0f)
        {
            justChanged = false;
        }
    }
}
