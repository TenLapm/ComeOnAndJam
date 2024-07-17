using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float setTime = 60;
    public float CurrentTime => setTime;

    private void Update()
    {
        if (Time.timeScale == 1.0f)
        {
            setTime -= Time.deltaTime;
        }
    }

    public void ResetTimer()
    {
        setTime = 60f;
    }
}
