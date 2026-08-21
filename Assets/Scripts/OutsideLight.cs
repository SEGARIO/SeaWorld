using UnityEngine;

public class OutsideLight : MonoBehaviour
{
    public Light _light;
    bool _canUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_canUp && _light.intensity <= 1)
        {
            _light.intensity += Time.deltaTime /3;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _canUp = true;
        }
    }
}
