using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPowerUps : MonoBehaviour
{
    public bool HavePowerUp = false;
    public bool usingPowerUs = false;
    private int type;
    private PowerUps PowerUp;
    private float duration;
    private Transform scale;
    private SpawnPointPowerUps spawnPointPowerUps;
    void Start()
    {
        
    }

    void Update()
    {
        UsingPowerUps();
        PowerUpsDuration();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUp" && !HavePowerUp)
        {
            Debug.Log("Pickup");
            Destroy(other.gameObject);
            HavePowerUp = true;
            PowerUp = other.GetComponent<PowerUpUi>().Powerup;
            spawnPointPowerUps.count--;
        }
        else if(other.tag == "SPPU")
        {
            spawnPointPowerUps = other.GetComponent<SpawnPointPowerUps>();
        }
    }

    public void UsingPowerUps()
    {
        
        if (Input.GetKeyDown(KeyCode.I) && HavePowerUp)
        {
            type = (int)PowerUp.type;
            HavePowerUp = false;
            switch(type)
            {
                case 0:
                    break;
                case 1:
                    duration = PowerUp.duration;
                    scale = transform;
                    transform.localScale = new Vector3(3f, 3f, 3f);
                    usingPowerUs = true;
                    break;
                case 2:
                    break;
            }
        }
    }

    public void PowerUpsDuration()
    {

        if (usingPowerUs)
        {
            duration -= Time.deltaTime;
        }
        if (duration <= 0 && usingPowerUs)
        {
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    usingPowerUs = false;
                    break;
                case 2:
                    break;
            }
        }
    }
}
