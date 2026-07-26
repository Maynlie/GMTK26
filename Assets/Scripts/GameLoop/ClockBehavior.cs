using UnityEngine;

public class ClockBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject aiguille;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float startTime = GameManager.Instance.GetSTartTime();
        float elapsed = Time.deltaTime / startTime;
        aiguille.transform.Rotate(new Vector3(-171.0f*elapsed, 0, 0));
    }
}
