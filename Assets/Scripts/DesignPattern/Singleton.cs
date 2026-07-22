using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	static T instance;

	public static T Instance {  get { return instance; } }

	private void Awake()
	{
		if(instance == null)
		{
			instance = this as T;
		}
		else
		{
			string className = nameof(T);
			Debug.LogError($"Instance of {className} is not null. Singleton must be the only one instance of {className}");
		}
	}
}
