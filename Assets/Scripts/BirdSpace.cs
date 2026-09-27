using UnityEngine;

public class BirdSpace : MonoBehaviour
{
    public float _limit;
    public float _limit2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > _limit)
        {
            this.transform.position = new Vector3(this.transform.position.x, _limit2, this.transform.position.z);
        }
    }
}
