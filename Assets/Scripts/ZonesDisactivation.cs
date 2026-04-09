using UnityEngine;

public class ZonesDisactivation : MonoBehaviour
{
    public GameObject _container;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _container.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Block")
        {
            other.transform.SetParent(_container.transform);
            
        }
    }
}
