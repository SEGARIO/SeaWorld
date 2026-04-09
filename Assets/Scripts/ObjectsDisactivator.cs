using System.Collections.Generic;
using UnityEngine;

public class ObjectsDisactivator : MonoBehaviour
{
    public GameObject _container;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _container.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _container.SetActive(false);
        }
    }


}
