using UnityEngine;
using UnityEngine.AI;

public class BeetleScript : MonoBehaviour
{
    public EnemyScript _enemysScript;
    public Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject)
        {
            _enemysScript.gameObject.GetComponent<NavMeshAgent>().speed = 0;
            _anim.SetTrigger("Out");
            Invoke("GetOut", 2);
        }
    }

    void GetOut()
    {
        _enemysScript.gameObject.GetComponent<NavMeshAgent>().speed = _enemysScript._enemy._speed ;
    }
}
