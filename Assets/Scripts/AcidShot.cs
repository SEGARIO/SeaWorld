using UnityEngine;

public class AcidShot : MonoBehaviour
{
    public GameObject _flack;
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
        Instantiate(_flack, new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }
}
