using UnityEngine;

public class WaterRise : MonoBehaviour
{
    blockChangeScriot[] _allBlocks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WaterElevate();
        _allBlocks = FindObjectsOfType<blockChangeScriot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            WaterElevate();
        }
    }

    void WaterElevate()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);

        for (int i = 0; i < _allBlocks.Length; i++)
        {
            _allBlocks[i].WaterRise();
         }
    }
}
