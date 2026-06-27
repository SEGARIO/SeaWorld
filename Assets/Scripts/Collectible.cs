using UnityEngine;

public class Collectible : MonoBehaviour
{
    PlayerLife _life;

    public float _lifeToHeal;
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
            _life = other.GetComponent<PlayerLife>();

            
            _life._life += _lifeToHeal;
            Destroy(gameObject);
        }
    }
}
