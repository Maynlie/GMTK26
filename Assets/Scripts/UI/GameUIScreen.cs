using UnityEngine;

public class GameUIScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void DebugButton(GameObject gameObject)
    {
        Debug.Log($"Button {gameObject.name} clicked!");
	}
}
