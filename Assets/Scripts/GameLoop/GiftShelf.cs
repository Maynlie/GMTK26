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

	public void ReturnGiftToPool(GameObject gift)
	{
		gift.transform.position = poolPosition.position;
		giftPool.Enqueue(gift);
	}

	public void GiveImpGift(LutinBehavior lutin, Vector2Int order)
	{
		if (lutin.lutinType == LutinBehavior.LutinType.Giselle)
		{
            order.y = order.y + 1;
            if (order.y > 3) order.y = 0;
        }

		int id = 0;

        GameObject gift = GetGiftAtCase(order.x, order.y);
		if (gift.name == "Boardgames(Clone)")
		{
			id = 0;
		}
		else if(gift.name == "Book(Clone)")
		{
            id = 1;
        }
        else if (gift.name == "Car(Clone)")
        {
            id = 2;
        }
        else if (gift.name == "Console(Clone)")
        {
            id = 5;
        }
        else if (gift.name == "Doll(Clone)")
        {
            id = 4;
        }
        else if (gift.name == "Hammer(Clone)")
        {
            id = 6;
        }
        else if (gift.name == "Lego(Clone)")
        {
            id = 7;
        }
        else if (gift.name == "MusicBox(Clone)")
        {
            id = 8;
        }
        else if (gift.name == "Piano(Clone)")
        {
            id = 9;
        }
        else if (gift.name == "Plush(Clone)")
        {
            id = 10;
        }
        else if (gift.name == "Quad(Clone)")
        {
            id = 11;
        }
        else if (gift.name == "SoccerBall(Clone)")
        {
            id = 12;
        }
        else if (gift.name == "Talky(Clone)")
        {
            id = 13;
        }
        else if (gift.name == "Triceratops(Clone)")
        {
            id = 3;
        }
        else if (gift.name == "TrojanHelmet(Clone)")
        {
            id = 14;
        }
        else if (gift.name == "TruckerHat(Clone)")
        {
            id = 15;
        }

		lutin.SetReceivedGiftId(id);

        RemoveGiftFromCase(order.x, order.y);
        gift.transform.SetParent(lutin.giftAnchor.transform);
        gift.transform.localPosition = Vector3.zero;

        switch (lutin.lutinType)
        {
            case LutinBehavior.LutinType.Bob:
				for(int i = 0; i < 4; i++)
				{
					SwapGift(order.x, i, order.x, Random.Range(0, 4));
                }
                break;
            case LutinBehavior.LutinType.Didier:
				for (int i = 0; i < 4; i++)
				{
					for(int j = 0; j < 4; j++)
                    {
                        SwapGift(i, j, Random.Range(0, 4), Random.Range(0, 4));
                    }
                }
                break;
        }

        for (int i = 0; i < 4; i++)
        {
			for (int j = 0; j < 4; j++)
			{
				if (giftPlaceholders[i, j].Gift == null)
				{
					for(int x = i; x > 0; x--)
					{
						SwapGift(x, j, x - 1, j);
					}
				}
            }
        }
    }

	public void ReturnGift(LutinBehavior lutin)
	{
		if (lutin.giftAnchor.transform.childCount == 0) return;
        GameObject gift = lutin.giftAnchor.transform.GetChild(0).gameObject;
		gift.transform.SetParent(null, true);

        ReturnGiftToPool(gift);


        for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (giftPlaceholders[i, j].Gift == null)
				{
					if(giftPool.Count > 0)
					{
                        giftPlaceholders[i, j].SetGift(giftPool.Dequeue());
                    }
                }

            }
		}
		lutin.CheckSuccessDelivery();
	}
	
}
