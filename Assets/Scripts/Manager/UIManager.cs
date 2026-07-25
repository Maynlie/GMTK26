using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject rootUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.ResetGameTime();
        GameManager.Instance.SetInGame(true);
        Time.timeScale = 1;

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.SetInGame(false);
    }

    public void Quit()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
