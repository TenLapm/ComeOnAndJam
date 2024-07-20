using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PowerUpUi : MonoBehaviour
{
    public PowerUps Powerup;
    public int type;
    public float duration;
    public SpawnPointPowerUps count;
    public float scale;
    public int instant;
    private float timer;
    [Header("LifeTime")]
    [SerializeField] private float maxTimer;


    void Start()
    {
        type = (int)Powerup.type;
        duration = (float)Powerup.duration;
        count = GetComponentInParent<SpawnPointPowerUps>();
        scale = (float)Powerup.scale;
        instant = (int)Powerup.instant;
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if( timer <= 0 )
        {
            Destroy(gameObject);
            count.count--;
        }
    }

    
}
