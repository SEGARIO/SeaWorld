using UnityEngine;

public class LookAtFixedPoint : MonoBehaviour
{
    public GameObject _pressA;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1));
        _pressA.transform.position = new Vector3(this.transform.position.x - 0.3f, _pressA.transform.position.y, _pressA.transform.position.z);
    }
}
