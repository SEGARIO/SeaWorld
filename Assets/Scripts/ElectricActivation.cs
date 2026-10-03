using UnityEngine;

public class ElectricActivation : MonoBehaviour
{
    public Animator _anim;
    public Renderer _rend;
    public Material _originalMat;
    public Material _NewMat;
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
        if (collision.gameObject.tag == "Bullet")
        {

            

            if (collision.gameObject.GetComponent<ElectricBullet>()._isElectric)
            {
                _anim.SetTrigger("Activate");
                GameFeel.Instance.PlayJuice(2, 0.5f);
                GameFeel.Instance.Flash(0.1f);
                _rend.material = _NewMat;
            }

          
        }
       
    }

   
}
