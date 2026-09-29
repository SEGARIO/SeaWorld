using UnityEngine;

public class ReactivatesController : MonoBehaviour
{
    public PlayerController _controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reactivation()
    {
        _controller.enabled = true;
    }
}
