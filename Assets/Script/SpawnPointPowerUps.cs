using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointPowerUps : MonoBehaviour
{
    public int count = 0;
    public bool isMax = false;
    private float Timer = 0;
    [SerializeField] private float axisX = 0;
    [SerializeField] private float axisY = 0;
    [SerializeField] private List<GameObject> PowerUp = new List<GameObject>();
    [SerializeField] private int limit = 5;
    [SerializeField] private float maxTimer = 5;
    [SerializeField] private float radius = 0.5f;


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
            Timer -= Time.deltaTime;
        }
        
    }

    void SpawnPowerUp()
    {
        if (count < limit && Timer <= 0)
        {
            int random = Random.Range(0, PowerUp.Count - 1);
            float randomX = Random.Range(-axisX / 2, axisX / 2);
            float randomY = Random.Range(-axisY / 2, axisY / 2);
            Vector2 pos = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
            Collider2D collider = Physics2D.OverlapCircle(pos, radius);

            if (collider == null)
            {
                GameObject newChild = Instantiate(PowerUp[random], pos, Quaternion.identity);
                newChild.transform.parent = transform;
                isMax = false;
                count++;
                Timer = maxTimer;
            };
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(axisX, axisY));
    }
}
