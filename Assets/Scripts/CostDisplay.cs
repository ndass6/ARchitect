using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostDisplay : MonoBehaviour
{
    [HideInInspector]
    public static CostDisplay Instance;

    private Text costText;

    public void Awake()
    {
        Instance = this;
        costText = GetComponent<Text>();
    }

    public void Start()
    {
        UpdateCost();
    }

    public void UpdateCost()
    {
        float totalCost = 0;
        foreach(Hologram hologram in FindObjectsOfType<Hologram>()) // TODO Make more efficient
            totalCost += hologram.Cost;
        costText.text = "<b>Total cost:</b> <i>$" + totalCost.ToString("F2") + "</i>";
    }
}
