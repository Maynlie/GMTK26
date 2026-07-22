using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{

	#region Properties

	#endregion

	#region Unity Methods

	private void Start()
	{

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
