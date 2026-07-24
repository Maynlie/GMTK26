using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Letter", menuName = "GMTK/Letter")]
public class Letter : ScriptableObject
{
	/// <summary>
	/// Content of the letter. Describes gift and its purpose. Can be used to display in the game.
	/// </summary>
	string content;
}
