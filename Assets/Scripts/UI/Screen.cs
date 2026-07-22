using UnityEngine;

public abstract class Screen : MonoBehaviour
{
	public void CancelScreen()
	{
		Destroy(this);
	}
}
