using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPowerUps : MonoBehaviour
{
    public bool HavePowerUp = false;
    void Start()
    {
        
    }

    void Update()
    {
        UsingPowerUps();
    }

    public void UsingPowerUps()
    {
        if (Input.GetKeyDown(KeyCode.I) && HavePowerUp)
        {
            HavePowerUp = false;
        }
    }
}
