using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Gift : ScriptableObject
{
	/// <summary>
	///	Letter associated with the gift.
	/// </summary>
	List<Letter> letters;
	/// <summary>
	/// Mesh corresponding to the gift with script or another. 
	/// </summary>
	GameObject prefabs;
}
