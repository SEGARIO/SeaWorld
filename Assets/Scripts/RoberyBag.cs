using UnityEngine;

public class RoberyBag : MonoBehaviour
{
    public GameObject _objectToActivate;
    public GameObject _objectToDisctivate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        _objectToActivate.SetActive(true);
        Destroy(_objectToDisctivate);
    }
}
