using System.Threading;
using UnityEngine;

public class HpCollider : MonoBehaviour
{
    public PlayerLife _life;
    public bool _isGivingLife;
    public float _speed;
    public GameObject _visual;
    GameObject _enemy;

    private void Update()
    {
        if(_isGivingLife && _life._life > 0)
        {
            _life._life += Time.deltaTime * _speed;
            _visual.SetActive(true);
            GameFeel.Instance.PlayJuice(1.5f, 0.3f);
        }
        else
        {
            _visual.SetActive(false);
        }

        if(_enemy == null)
        {
            _isGivingLife = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            _isGivingLife = true;
            _enemy = collision.gameObject;
        }

        if (collision.gameObject == null)
        {
            _isGivingLife = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _isGivingLife = false;
        }
    }
}
