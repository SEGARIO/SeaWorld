using System;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    public GameObject _target;
    public bool _followsGun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {if(!_followsGun)
        {

            transform.LookAt(_target.transform);
        }
        else
        {
            if (Vector3.Distance(transform.position, _target.transform.position) > 2f)
            {
                transform.LookAt(_target.transform);
            }
            else
            {

            }
        }
       
    }
}
