using UnityEngine;

public class RandomAppear : MonoBehaviour
{
    public int _random;
    public GameObject[] _objects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _random = Random.Range(0, _objects.Length);

        for(int i = 0; i < _objects.Length; i++)
        {
            if(i != _random)
            {
                Destroy(_objects[i]);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
