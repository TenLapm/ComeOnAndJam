using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointPowerUps : MonoBehaviour
{
    public int count = 0;
    public bool isMax = false;
    [SerializeField] private float axisX = 0;
    [SerializeField] private float axisY = 0;
    [SerializeField] private List<GameObject> PowerUp = new List<GameObject>();
    [SerializeField] private int limit = 5;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (count >= limit)
        {
            isMax = true;
        }
        if (!isMax)
        {
            SpawnPowerUp();
        }
        
    }

    void SpawnPowerUp()
    {
        if (count < limit)
        {
            int random = Random.Range(0, PowerUp.Count - 1);
            float randomX = Random.Range(-axisX / 2, axisX / 2);
            float randomY = Random.Range(-axisY / 2, axisY / 2);
            Vector2 pos = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
            Instantiate(PowerUp[random], pos, Quaternion.identity);
            isMax = false;
            count++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(axisX, axisY));
    }
}
