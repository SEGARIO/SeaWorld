using UnityEngine;

public class RestaurantScript : MonoBehaviour
{
    public string _restaurantName;
    public bool _isAdress;

    public GameObject _ActiveRestaurant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isAdress)
        {
            _ActiveRestaurant.SetActive(true);
        }
        else
        {
            _ActiveRestaurant.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isAdress && other.gameObject.tag == "Player")
        {
            _ActiveRestaurant.SetActive(false);
            FindObjectOfType<MealGestion>()._hasMeal = true;
            _isAdress = false;
        }
    }
}
