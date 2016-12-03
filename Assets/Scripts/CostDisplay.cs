using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostDisplay : MonoBehaviour
{
    [HideInInspector]
    public static CostDisplay Instance;

    private Text costText;
    private float totalCost;

    public void Awake()
    {
        Instance = this;
        totalCost = 0;
        costText = GetComponent<Text>();
    }

    public void Start()
    {
        UpdateCost(0);
    }

    public void UpdateCost(float cost)
    {
        //float totalCost = 0;
        costText.fontSize = 20;
        //foreach (Hologram hologram in FindObjectsOfType<Hologram>()) {
            totalCost += cost;
            costText.text = "<b>Total cost:</b> <i>$" + totalCost.ToString("F2") + "</i>";
        //}
        // TODO Make more efficient    
    }
}
