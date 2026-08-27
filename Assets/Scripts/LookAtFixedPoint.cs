using UnityEngine;

public class LookAtFixedPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1));
    }
}
