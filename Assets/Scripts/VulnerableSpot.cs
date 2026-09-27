using UnityEngine;

public class VulnerableSpot : MonoBehaviour
{
    public Renderer _rend;
    public Material _originalColors;
    public Material _hitColor;
    public Levierthan _enemyScript;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
      _rend.material = _originalColors;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            _rend.material = _hitColor;

            Invoke("OriginalColors", 0.05f);
            _enemyScript._life -= 1;

           
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Water")
        {

            _enemyScript._life = 0;

        }
    }

    void OriginalColors()
    {
        _rend.material = _originalColors;
    }
}
