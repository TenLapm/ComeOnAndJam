using UnityEngine;
using TMPro;

public class DrawingTracker : MonoBehaviour
{
    public TextMeshProUGUI percentageText; 
    public int totalPixels = 1920 * 1080;
    private int drawnPixels = 0;

    private void Start()
    {
        UpdatePercentage();
    }

    public void AddDrawnPixels(int amount)
    {
        drawnPixels += amount;
        UpdatePercentage();
        Debug.Log("Drawn Pixels: " + drawnPixels); 
    }

    private void UpdatePercentage()
    {
        float percentage = ((float)drawnPixels / totalPixels) * 100f;
        percentageText.text = "Drawn: " + percentage.ToString("F2") + "%";
        Debug.Log("Updated Percentage: " + percentage); 
    }
}
