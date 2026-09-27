using UnityEngine;

public class RandomRotationAtAstart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
