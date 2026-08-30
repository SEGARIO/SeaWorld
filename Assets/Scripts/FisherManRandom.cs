using UnityEngine;

public class FisherManRandom : MonoBehaviour
{
    public GameObject[] _objects;
    public GameObject _currentObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("ChangeObjects", 8);
    }

   
    

    void ChangeObjects()
    {
        _currentObject = _objects[Random.Range(0, _objects.Length)];
        _currentObject.SetActive(true);
        for (int i = 0; i < _objects.Length; i++)
        {
            if( _objects[i] != _currentObject)
            {
                _objects[i].SetActive(false);
            }
        }
        Invoke("ChangeObjects", 8);
    }
}
