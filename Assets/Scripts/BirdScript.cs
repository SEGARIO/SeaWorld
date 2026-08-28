using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Animator _anim;
    public Transform _player;
    public float _range;
    public ParticleSystem _smoke;
    bool canPlay = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        //Debug.Log("Distance au player : " + distance);
        _anim.SetInteger("Random", Random.Range(0, 3));

        if (distance <= _range)
        {
            Fly();
        }
    }

    void Fly()
    {
        if(canPlay)
        {
            _anim.SetBool("Fly", true);
            _smoke.Play();
            canPlay = false;
        }
        
        
        Invoke("Destroyer", 2.5f);
    }

    void Destroyer()
    {
        Destroy(this.gameObject);
    }
}
