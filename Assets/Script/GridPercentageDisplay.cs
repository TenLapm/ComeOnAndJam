using TMPro;
using UnityEngine;

public class GridPercentageDisplay : MonoBehaviour
{
    public GridManager gridManager;
    public TextMeshProUGUI percentageText;

    void Start()
    {
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
            if (gridManager == null)
            {
                Debug.LogError("GridManager component not found in the scene.");
            }
        }

        if (percentageText == null)
        {
            Debug.LogError("Percentage TextMeshProUGUI component not assigned.");
        }
    }

    void Update()
    {
        //float markedPercentage = gridManager.GetMarkedPercentage();
        //percentageText.text = $"Grid Walked: {markedPercentage:F2}%";
    }
}
