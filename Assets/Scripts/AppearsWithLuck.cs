using UnityEngine;

public class AppearsWithLuck : MonoBehaviour
{
    public int _luck;
    int _random;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _random = Random.Range(0, _luck);
        if(_random != 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
