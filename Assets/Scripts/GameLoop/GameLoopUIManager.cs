using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class GameLoopUIManager : MonoBehaviour
{
    public InputActionReference pauseActionRef;
    bool isPaused = false;
    public GameObject pauseLayer, gameLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseActionRef.action.started += TogglePause;
    }

    public void Resume()
    {
        isPaused = false;
        pauseLayer.SetActive(false);
        gameLayer.SetActive(true);
    }

    public void TogglePause(InputAction.CallbackContext context) 
    {
        isPaused = !isPaused;
        pauseLayer.SetActive(isPaused);
        gameLayer.SetActive(!isPaused);
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
