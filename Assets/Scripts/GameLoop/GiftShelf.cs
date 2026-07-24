using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GiftShelf : MonoBehaviour
{	
	GiftPlaceholder[,] giftPlaceholders;

	[SerializeField] int width = 4;
	[SerializeField] int height = 4;
	[SerializeField] Queue<GameObject> giftPool;
	[SerializeField] List<Gift> gifts;
	[SerializeField] Transform poolPosition;

	private void Start()
	{
		GiftPlaceholder[] placeholders = GetComponentsInChildren<GiftPlaceholder>();
		giftPlaceholders = new GiftPlaceholder[width, height];
		giftPool = new Queue<GameObject>();

		for (int i = 0; i < height; i++)
		{
			for(int j = 0; j < width; j++)
			{
				giftPlaceholders[j, i] = placeholders[j * height + i];
			}
		}

		ShuffleGifts();

		foreach (Gift gift in gifts)
		{
			giftPool.Enqueue(GameObject.Instantiate(gift.Prefabs));
		}

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				giftPlaceholders[j, i].SetGift(giftPool.Dequeue());
			}
		}
	}

	void ShuffleGifts()
	{
		for (int i = 0; i + 1 < gifts.Count - 1; i++)
		{
			int j = Random.Range(i + 1, gifts.Count - 1); // Max exclusive, so -2 to avoid out of range
			Gift temp = gifts[i];
			gifts[i] = gifts[j];
			gifts[j] = temp;
		}
	}

	public GameObject GetGiftAtCase(int x, int y)
	{
		Debug.Log($"{giftPlaceholders[x, y].Gift.name}");
		return giftPlaceholders[x, y].Gift;
	}

	public void RemoveGiftFromCase(int x, int y)
	{
		giftPlaceholders[x, y].RemoveGift();
	}

	public void SwapGift(int xA, int yA, int xB, int yB)
	{
		GameObject temp = giftPlaceholders[xA, yA].Gift;
		giftPlaceholders[xA, yA].SetGift(giftPlaceholders[xB, yB].Gift);
		giftPlaceholders[xB, yB].SetGift(temp);
	}
}
