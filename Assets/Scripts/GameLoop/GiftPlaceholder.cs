using UnityEngine;

public class GiftPlaceholder : MonoBehaviour
{
	GameObject giftPrefab;

	public GameObject Gift { get => giftPrefab; }

	public void SetGift(GameObject gift)
	{
		giftPrefab = gift;
		giftPrefab.transform.position = transform.position;
	}

	public GameObject RemoveGift()
	{
		GameObject temp = giftPrefab;
		giftPrefab = null;
		return temp;
	}
}
