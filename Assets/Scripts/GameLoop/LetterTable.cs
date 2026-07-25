using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetterTable : MonoBehaviour
{
    [SerializeField] GameObject[] lettersAnchor;
	[SerializeField] GameObject[] lettersObject;
	[SerializeField] GameObject letterPrefab;


	void Start()
    {
		lettersObject = new GameObject[lettersAnchor.Length];

		for(int i = 0;  i < lettersAnchor.Length; i++) 
        {
			lettersObject[i] = Instantiate(letterPrefab, lettersAnchor[i].transform.position, lettersAnchor[i].transform.rotation, lettersAnchor[i].transform);
		}
    }

	public void SpawnLetter(int anchorID)
    {
		GameObject anchor = lettersAnchor[anchorID];
		lettersObject[anchorID] =  Instantiate(letterPrefab, anchor.transform.position, anchor.transform.rotation, anchor.transform);
	}

    public void RemoveLetter(int anchorID)
    {
		GameObject letter = lettersObject[anchorID];
		lettersObject[anchorID] = null;
		Destroy(letter);
	}
}
