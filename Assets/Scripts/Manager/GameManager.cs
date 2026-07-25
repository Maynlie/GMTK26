using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

	#region Properties

	#endregion

	#region Unity Methods
	float gameTime = 60.0f * 5.0f;
	bool inGame = false;
	private void Start()
	{

	}

	public void ResetGameTime()
	{
		gameTime = 60.0f * 5.0f;
	}


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
