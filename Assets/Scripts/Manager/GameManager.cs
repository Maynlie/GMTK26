using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

	float startTime = 60.0f * 5.0f;
	float gameTime;
	bool inGame = false;

	[SerializeField] LetterData[] letterData;
	public LetterData selectedGift;
	public string selectedToogle;

	#region Properties

	#endregion

	#region Unity Methods

	private void Start()
	{
		gameTime = startTime;
		RandomGift();
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

	public void RandomGift()
	{
		selectedGift = letterData[Random.Range(0, letterData.Length)];
	}

	public LetterData GetGift()
	{
		return selectedGift;
	}

}
