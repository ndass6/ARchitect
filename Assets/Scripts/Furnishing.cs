using UnityEngine;
using System.Collections;

public class Furnishing : MonoBehaviour
{
    [SerializeField]
    private Hologram prefab;

    /// <summary>
    /// Closes the furnishing menu and creates a hologram corresponding to the selected furnishing.
    /// </summary>
    public void Select()
    {
        FurnishingMenu.Instance.CloseMenu();
        Instantiate(prefab, Camera.main.transform.position + Camera.main.transform.forward * 5, Quaternion.identity);
        CostDisplay.Instance.UpdateCost(prefab.Cost);
    }
}
