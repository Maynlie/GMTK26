using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour
{

	/// <summary>
	/// Dictionary containing all the gifts, accessible by their string identifier.
	/// </summary>
	[SerializeField]	
	Dictionary<string, Gift> gifts;
}
