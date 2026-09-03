using UnityEngine;

public class Credits : MonoBehaviour
{
    public float _speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + _speed, this.transform.position.z);
    }
}
