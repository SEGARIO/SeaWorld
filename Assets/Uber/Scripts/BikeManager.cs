using TMPro;
using UnityEngine;

public class BikeManager : MonoBehaviour
{
    public static int _money;
    public TMP_Text _moneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moneyText.text = "" +_money;
    }
}
