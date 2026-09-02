using System;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    public GameObject _target;
    public bool _followsGun;
    public TriggerShoot _shoot;
    bool _canShoot;

    [Header("Arms")]
    public Transform _arm1;
    public Transform _arm2;

    public Transform _refForward;
    public Transform _gun;
    public float _distance;
    public GameObject _viserRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _arm2.transform.localRotation = Quaternion.Euler(-10.801f, 87.681f, 10.33f);
        _arm1.transform.localRotation = Quaternion.Euler(37.822f, 37.822f, 576.116f);
    }

    // Update is called once per frame
    void Update()
    {if(!_followsGun)
        {

            transform.LookAt(_target.transform);
        }
        else
        {

            if (Vector3.Distance(transform.position, _target.transform.position) > 2 && Vector3.Distance(_gun.position, _refForward.transform.position) < _distance)
            {
                transform.LookAt(_target.transform);
                _canShoot = true;
                _arm2.transform.localRotation = Quaternion.Euler(-29.828f, -20.904f, 18.874f);
                _shoot.enabled = true;
                Debug.Log("Proche");
                _viserRenderer.SetActive(true);
            }
            else
            {
                _shoot.enabled = false;
                _arm2.transform.localRotation = Quaternion.Euler(-10.801f, 87.681f, 10.33f);
                _arm1.transform.localRotation = Quaternion.Euler(37.822f, 37.822f, 576.116f);
                Debug.Log("Loins");
                _canShoot = false;
                _viserRenderer.SetActive(false);
            }

            Debug.Log(_distance);
        }
       
    }
}
