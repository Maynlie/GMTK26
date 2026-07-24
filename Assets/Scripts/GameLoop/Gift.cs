using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gift", menuName = "GMTK/Gift")]
public class Gift : ScriptableObject
{
	/// <summary>
	///	Letter associated with the gift.
	/// </summary>
	[SerializeField] List<Letter> letters;
	/// <summary>
	/// Mesh corresponding to the gift with script or another. 
	/// </summary>
	[SerializeField] GameObject prefabs;

	public GameObject Prefabs { get => prefabs; }	
}
