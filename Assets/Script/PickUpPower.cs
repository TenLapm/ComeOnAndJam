using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpPower : MonoBehaviour
{
    private InventoryPowerUps IPU;
    void Start()
    {
        IPU = GetComponent<InventoryPowerUps>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PowerUp")
        {
            Debug.Log("Pickup");
            Destroy(other.gameObject);
            IPU.HavePowerUp = true;
        }
    }
}
