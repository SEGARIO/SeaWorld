using UnityEngine;

public class FrogScript : MonoBehaviour
{
    public Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetInteger("Random", Random.Range(0, 3));
    }
}
