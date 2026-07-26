using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LetterUI : MonoBehaviour
{
    Quaternion toFlip;
    [SerializeField]float rotationSpeed = 180f;

	[SerializeField] List<string> childNameList;
	[SerializeField] List<string> headerList;

	[SerializeField] TextMeshProUGUI headerText;
	[SerializeField] TextMeshProUGUI contentText;
	[SerializeField] TextMeshProUGUI childText;

	[SerializeField] List<Toggle> toggles;

	private void Start()
	{
		WriteLetter();
	}

	// Update is called once per frame
	void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, toFlip, rotationSpeed * Time.deltaTime);
	}

    public void Flip()
    {
		
        if(transform.localRotation == Quaternion.Euler(0, 180, 0))
			toFlip = Quaternion.Euler(0, 0, 0);
		else
			toFlip = Quaternion.Euler(0, 180, 0);
	}

	public void WriteLetter()
	{
		headerText.text = headerList[Random.Range(0, headerList.Count)];
		List<string> letterContent =  GameManager.Instance.GetGift().contentList;
		contentText.text = letterContent[Random.Range(0, letterContent.Count)];
		childText.text = childNameList[Random.Range(0, childNameList.Count)];
	}

	public void SelectedToogle(string toogleID)
	{
		if(GameManager.Instance.selectedToogle != "")
		{
			foreach(Toggle toggle in toggles)
			{
				if($"Line{GameManager.Instance.selectedToogle}" == toggle.name)
				{
					toggle.isOn = false;
				}
			}
            
        }
		if(GameManager.Instance.selectedToogle == toogleID)
		{
			GameManager.Instance.selectedToogle = "";
        }
		else
		{
            GameManager.Instance.selectedToogle = toogleID;
        }
    }

	public void SendLetter()
	{
		if(GameManager.Instance.selectedToogle != "")
		{
            GameManager.Instance.AssignOrderToLutin();
        }
	}
}
