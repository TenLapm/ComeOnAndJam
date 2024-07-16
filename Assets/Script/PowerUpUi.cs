using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUi : MonoBehaviour
{
    public PowerUps Powerup;
    public int type;
    public float duration;
    public SpawnPointPowerUps count;
    void Start()
    {
        type = (int)Powerup.type;
        duration = (float)Powerup.duration;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
