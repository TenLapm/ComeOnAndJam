using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeCount : MonoBehaviour
{
    [SerializeField] private int globeCount = 6;
    private SpawnPointPowerUps[] count;
    private int num = 0;
    void Start()
    {
        count = GetComponentsInChildren<SpawnPointPowerUps>();
    }

    
    void Update()
    {
        Count();
    }

    void Count()
    {
        num = 0;
        foreach (SpawnPointPowerUps ps in count)
        {
            num += ps.count;
        }
        if(num >= globeCount)
        {
            foreach (SpawnPointPowerUps ps in count)
            {
                ps.isMax = true;
            }
        }
        else
        {
            foreach (SpawnPointPowerUps ps in count)
            {
                ps.isMax = false;
            }
        }
    }
}
