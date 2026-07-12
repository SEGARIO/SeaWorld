using UnityEngine;

public class SpaceShipLife : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Enemy")
        {
            GameFeel.Instance.PlayJuice(1.5f, 0.6f);
            GameFeel.Instance.Flash(0.5f);

        }
    }
}
