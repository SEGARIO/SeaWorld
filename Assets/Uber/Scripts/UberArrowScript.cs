using UnityEngine;

public class UberArrowScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindObjectOfType<MealGestion>()._hasMeal)
        {
            transform.LookAt(FindObjectOfType<BuildingPicker>().currentrestaurant.transform.position);
        }
        else
        {
            transform.LookAt(FindObjectOfType<BuildingPicker>()._client.transform.position);
        }
    }
}
