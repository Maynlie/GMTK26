using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gift", menuName = "GMTK/Gift")]
public class Gift : ScriptableObject
{
	/// <summary>
	/// Mesh corresponding to the gift with script or another. 
	/// </summary>
	[SerializeField] GameObject prefabs;
	[SerializeField] int giftId;


	public GameObject Prefabs { get => prefabs; }

	public int GetID() {  return giftId; }
}
