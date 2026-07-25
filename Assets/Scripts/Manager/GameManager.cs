using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

	#region Properties

	#endregion

	#region Unity Methods
	float startTime = 60.0f * 5.0f;
    float gameTime;
	bool inGame = false;
	private void Start()
	{
		gameTime = startTime;
    }

	public void ResetGameTime()
	{
		gameTime = startTime;
	}

	public float GetGameTime() { return gameTime; }
	public float GetSTartTime() { return startTime; }


    public void SetInGame(bool value) {
		inGame = value;
	}

    private void Update()
    {
		if (!inGame) return;
        gameTime -= Time.deltaTime;
		if( gameTime <= 0 )
		{
			// Handle game over logic
			SceneManager.LoadScene(2);
			inGame = false;
		}
    }

    #endregion

    #region Global methods

    public void QuitGame()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	#endregion

}
