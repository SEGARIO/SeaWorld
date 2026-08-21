using UnityEngine;

public class AdvancingScript : MonoBehaviour
{
    public float _speed;
    public Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + Time.deltaTime * _speed);

        if(this.transform.position.z >= 2367)
        {
            _anim.enabled = true;
            _anim.SetTrigger("Up");
        }
    }
}
