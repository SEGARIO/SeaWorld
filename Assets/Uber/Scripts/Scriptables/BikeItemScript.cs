using UnityEngine;

public class BikeItemScript : MonoBehaviour
{
    public MotorcycleController _controller;
    public bool _isStat;
    public SO_BikeItem _item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = FindObjectOfType<MotorcycleController>();
        if(_isStat)
        {
            _controller.moveSpeed += _item._speed;
            _controller.acceleration += _item._acceleration;
            _controller.brakeAcceleration += _item._frein;
            _controller.steeringSpeed += _item._turn *3;
            FindObjectOfType<MealGestion>()._maxTimer += _item._time;
        }

        if(_item._name == "Car Breaker")
        {
            FindObjectOfType<MotorcycleCrash>()._canCrash = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
