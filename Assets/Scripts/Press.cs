using UnityEngine;

public class Press : MonoBehaviour
{
    public Transform _ref;
    public Transform _refP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _ref.position;
        this.transform.LookAt(_refP.position);
    }
}
