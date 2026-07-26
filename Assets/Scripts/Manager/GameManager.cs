using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class GameManager : Singleton<GameManager>
{

	float startTime = 60.0f * 5.0f;
	float gameTime;
	bool inGame = false;

	[SerializeField] LetterData[] letterData;
    public LutinBehavior[] lutins;
    public LetterData selectedGift;
	public string selectedToogle;
	public KioskController kioskController;

	int score = 0;

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

	public int GetScore() { return score; }

	public void SuccesDelivery()
	{
		score++;
		kioskController.UpdateScoreUI();
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

	public void RandomGift()
	{
		selectedGift = letterData[Random.Range(0, letterData.Length)];
	}

	public LetterData GetGift()
	{
		return selectedGift;
	}

	public void SetLutins(LutinBehavior[] l, KioskController k)
	{
		int index = 0;
        foreach(LutinBehavior lu in l)
		{
			lutins[index] = lu;
			index++;
		}
		kioskController = k;
	}

	public void AssignOrderToLutin()
	{
        if (Time.timeScale == 0) return; // Do not give orders if the game is paused
        char firstChar = selectedToogle.ToCharArray()[0];
        char secondtChar = selectedToogle.ToCharArray()[1];
		int firstPos = firstChar - '0' - 1;
		int secondPos = secondtChar - 'A' ;
         foreach (LutinBehavior lut in lutins)
        {
            if (lut.GetCurrentState() == LutinBehavior.LutinState.WaitingForOrder)
            {
				kioskController.CloseLetter();
                lut.ReceiveOrder(new Vector2Int(firstPos, secondPos), selectedGift.GetID());
				RandomGift();
            }
        }
    }

}
