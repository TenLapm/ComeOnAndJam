using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InventoryPowerUps : MonoBehaviour
{
    public bool HavePowerUp = false;
    public bool usingPowerUs = false;
    private int type;
    private PowerUps PowerUp;
    private float duration;
    private Transform scale;
    private SpawnPointPowerUps spawnPointPowerUps;
    private List<GameObject> Clone = new List<GameObject>();
    private GameObject Gclone;
    private int instant;
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
            HavePowerUp = true;
            PowerUp = other.GetComponent<PowerUpUi>().Powerup;
            spawnPointPowerUps = other.GetComponent<PowerUpUi>().count;
            Gclone = PowerUp.Clone;
            spawnPointPowerUps.count--;
            instant = other.GetComponent<PowerUpUi>().instant;
            Destroy(other.gameObject);
        }
        
    }

    public void UsingPowerUps()
    {
        if(instant == 1 && HavePowerUp)
        {
            type = (int)PowerUp.type;
            HavePowerUp = false;
            switch (type)
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

                    usingPowerUs = true;
                    break;
                case 3:

                    for (int i = 0; i < PowerUp.scale; i++)
                    {
                        float axisz = (360.0f / (PowerUp.scale + 1));
                        GameObject c = Instantiate(Gclone, transform.position, Quaternion.Euler(new Vector3(0, 0, (axisz * (i + 1)) + transform.rotation.eulerAngles.z)));
                        duration = PowerUp.duration;
                        Clone.Add(c);
                    }
                    usingPowerUs = true;
                    break;
            }
        }
        if(Input.GetKeyDown(KeyCode.I) && HavePowerUp)
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

                    usingPowerUs = true;
                    break;
                case 3:
                    
                    for(int i = 0; i < PowerUp.scale; i++)
                    {
                        float axisz = (360.0f / (PowerUp.scale + 1));
                        GameObject c = Instantiate(Gclone, transform.position, Quaternion.Euler(new Vector3(0, 0, (axisz * (i + 1)) + transform.rotation.eulerAngles.z)));
                        duration = PowerUp.duration;
                        Clone.Add(c); 
                    }
                    usingPowerUs = true;
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
                    usingPowerUs = false;
                    break;
                case 3:
                    usingPowerUs = false;
                    for (int i = 0; i < PowerUp.scale; i++)
                    {
                        Destroy(Clone[i]);
                    }
                    Clone.Clear();
                    break;
            }
        }
    }
}
