using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public float _speedX;
    public float _speedY;
    public float _speedZ;
    Vector3 origin;

    public float _limit;
    public TrailRenderer _trail;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = new Vector3(this.transform.position.x, -_limit, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x + _speedX, this.transform.position.y + _speedY, this.transform.position.z + _speedZ);

        if (this.transform.position.y <= _limit)
        {
            _trail.time = 0;
            this.transform.position = origin;
            Invoke("ResetTrail", 0.1f);
        }
    }

    void ResetTrail()
    {
        _trail.time = 0.1f;
    }
}
