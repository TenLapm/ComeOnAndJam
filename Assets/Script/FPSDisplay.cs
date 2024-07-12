using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TMP_Text fpsText;
    public float hudRefreshRate = 0.5f; 

    private float timer;
    private int frames;
    private float fps;

    void Start()
    {
        timer = 0f;
        frames = 0;
        fps = 0f;

        if (fpsText == null)
        {
            Debug.LogError("FPSDisplay: TextMeshPro component not assigned!");
            enabled = false; 
        }
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;
        frames++;

        if (timer > hudRefreshRate)
        {
            fps = frames / timer;
            fpsText.text = $"FPS: {fps:F1}";

            timer = 0f;
            frames = 0;
        }
    }
}
