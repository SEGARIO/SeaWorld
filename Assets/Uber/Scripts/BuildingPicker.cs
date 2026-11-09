using TMPro;
using UnityEngine;

public class BuildingPicker : MonoBehaviour
{
    BuildingScript[] buildings;
    RestaurantScript[] restaurants;
    public BuildingScript currentBuilding;
    public RestaurantScript currentrestaurant;
    public TMP_Text _adressText;
    public GameObject _client;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildings = FindObjectsByType<BuildingScript>(FindObjectsSortMode.None);
        restaurants = FindObjectsByType<RestaurantScript>(FindObjectsSortMode.None);
        NewAdress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewAdress()
    {
        
        currentrestaurant = restaurants[Random.Range(0, restaurants.Length)];
        currentBuilding = buildings[Random.Range(0, buildings.Length)];
        currentrestaurant._isAdress = true;
        _adressText.text = currentrestaurant._restaurantName+ " for "+ currentBuilding._number + ", " + currentBuilding._roadName;
        _client.transform.position = currentBuilding._clientLocation.position;
    }
}
