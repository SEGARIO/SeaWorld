using UnityEngine;

public class BirdSpace : MonoBehaviour
{
    public float _limit;
    public float _limit2;
    public float _speed;
    public Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _anim.speed = Random.Range(0.8f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x + 
           _speed, this.transform.position.y, this.transform.position.z);
        if (this.transform.position.x > _limit)
        {
            this.transform.position = new Vector3(_limit2, this.transform.position.y, this.transform.position.z);
        }
    }
}
