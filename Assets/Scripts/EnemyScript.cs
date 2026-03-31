using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject _deathParticle;
    public SO_Enemy _enemy;
    public int _life;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _life = _enemy._life;
    }

    // Update is called once per frame
    void Update()
    {
        if(_life <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameFeel.Instance.PlayJuice(1.5f, 0.3f);

        //GameFeel.Instance.Flash(0.1f);
        Instantiate(_deathParticle, this.transform.position, Quaternion.identity);
        Destroy(gameObject);    
    }
}
