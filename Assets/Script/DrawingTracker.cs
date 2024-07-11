using UnityEngine;
using TMPro;

public class DrawingTracker : MonoBehaviour
{
    public TextMeshProUGUI percentageText; // Reference to the UI Text element
    public int totalPixels = 1920 * 1080; // Total number of pixels on the canvas
    private int drawnPixels = 0;

    private void Start()
    {
        UpdatePercentage();
    }

    public void AddDrawnPixels(int amount)
    {
        drawnPixels += amount;
        UpdatePercentage();
        Debug.Log("Drawn Pixels: " + drawnPixels); // Debug log to check drawn pixels
    }

    private void UpdatePercentage()
    {
        float percentage = ((float)drawnPixels / totalPixels) * 100f;
        percentageText.text = "Drawn: " + percentage.ToString("F2") + "%";
        Debug.Log("Updated Percentage: " + percentage); // Debug log to check percentage
    }
}
