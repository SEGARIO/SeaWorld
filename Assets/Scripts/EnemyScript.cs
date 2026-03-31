using UnityEngine;

public class EnemyScript : MonoBehaviour
{
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
        Destroy(gameObject);    
    }
}
