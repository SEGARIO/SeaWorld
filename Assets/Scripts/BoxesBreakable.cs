using UnityEngine;

public class BoxesBreakable : MonoBehaviour
{
    float _life;
    public float _maxLife;
    public Renderer _rend;
    public Material[] _materials;
    public GameObject _particle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _life = _maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if(_life < (_maxLife/3))
        {
            _rend.material = _materials[2];
        }
        if (_life < (_maxLife / 3) * 2 && _life > _maxLife / 3)
        {
            _rend.material = _materials[1];
        }

        if(_life <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Instantiate(_particle, this.transform.position, Quaternion.identity);
        GameFeel.Instance.PlayJuice(1.5f, 0.3f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _life -= 1;
        }
    }
}
