using UnityEngine;

public class DestroyUselessFace : MonoBehaviour
{
    bool isColliding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!isColliding)
        {
            Debug.Log("Don't detect anything");
            Destroy(GetComponent<BoxCollider>());
            Destroy(GetComponent<Rigidbody>());
            Destroy(this);
        }
    }

    private void Destroyer(GameObject _g)
    {
        Destroy(_g.gameObject);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Face")
        {
            isColliding = true;
            Destroyer(collision.gameObject);

        }
        else
        {
           

        }
    }
}
