using UnityEngine;

public class NormalLookAt : MonoBehaviour
{
    public Transform _target;
    public bool _canFollowY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_canFollowY)
        {
            transform.LookAt(_target.position);
        }
        else
        {
            transform.LookAt(new Vector3(_target.position.x,this.transform.position.y, _target.position.z));
        }
        
        
    }
}
