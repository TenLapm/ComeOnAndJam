using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUi : MonoBehaviour
{
    public PowerUps Powerup;
    public int type;
    public float duration;
    public SpawnPointPowerUps count;
    public float scale;


    void Start()
    {
        type = (int)Powerup.type;
        duration = (float)Powerup.duration;
        count = GetComponentInParent<SpawnPointPowerUps>();
        scale = (float)Powerup.scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
