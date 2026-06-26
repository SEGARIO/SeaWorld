using UnityEngine;

public class Stone : MonoBehaviour
{
    public bool _isLuminous;
    public Renderer _rend;
    public Material[] _mat;
    public LuminousStones _luminous;
    public int i;
    public ParticleSystem _particle;
    
    public AudioSource _source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isLuminous)
        {
            _rend.material = _mat[0];
        }
        else
        {
            _rend.material = _mat[1];
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if(!_isLuminous)
            {
                _isLuminous = true;
                _particle.Play();
                _source.Play();
                _luminous.Activate(i);
            }
           
        }
    }
}
