using UnityEngine;

public class BuildingPicker : MonoBehaviour
{
    BuildingScript[] buildings;
    public BuildingScript currentBuilding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildings = FindObjectsByType<BuildingScript>(FindObjectsSortMode.None);
        NewAdress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewAdress()
    {
        
        currentBuilding = buildings[Random.Range(0, buildings.Length)];
    }
}
