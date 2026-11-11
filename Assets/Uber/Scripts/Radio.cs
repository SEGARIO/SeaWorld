
using UnityEngine;
using UnityEngine.InputSystem;

public class Radio : MonoBehaviour
{
    [Header("Radio")]
    public AudioSource radioSource;
    public AudioClip[] stations;

    private int currentStation = 0;

    void Start()
    {
        if (stations.Length > 0)
            PlayStation(0);
    }

    void Update()
    {
        if (Mouse.current != null)
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
                NextStation();

            if (Mouse.current.leftButton.wasPressedThisFrame)
                PreviousStation();
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.rightTrigger.wasPressedThisFrame)
                NextStation();

            if (Gamepad.current.leftTrigger.wasPressedThisFrame)
                PreviousStation();
        }
    }

    void NextStation()
    {
        if (stations.Length == 0) return;

        currentStation++;

        if (currentStation >= stations.Length)
            currentStation = 0;

        PlayStation(currentStation);
    }

    void PreviousStation()
    {
        if (stations.Length == 0) return;

        currentStation--;

        if (currentStation < 0)
            currentStation = stations.Length - 1;

        PlayStation(currentStation);
    }

    void PlayStation(int index)
    {
        if (radioSource == null || stations[index] == null) return;

        radioSource.clip = stations[index];
        radioSource.Play();

        Debug.Log("Radio : " + stations[index].name);
    }
}
