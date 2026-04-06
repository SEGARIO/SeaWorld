using UnityEngine;
using UnityEngine.UI;

public class LocalisationText : MonoBehaviour
{
    public string[] _names;
     WaterRise _water;
    public Text _text;
    public Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _water = FindObjectOfType<WaterRise>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            

            if(_water.gameObject.transform.position.y <= -13)
            {
                _text.text = _names[0];
            }
            else
            {
                _text.text = _names[(int)_water.gameObject.transform.position.y + 13];
            }

            _anim.SetTrigger("Activate");
        }
    }
}
