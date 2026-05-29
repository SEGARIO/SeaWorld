using UnityEngine;

public class Casescript : MonoBehaviour
{
    public GameObject[] _cases;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int _random = Random.Range(0 , _cases.Length);  
        for (int i = 0; i < _cases.Length; i++)
        {
            if(i != _random)
            {
                Destroy(_cases[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
