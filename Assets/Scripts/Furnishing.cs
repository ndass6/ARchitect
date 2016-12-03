using UnityEngine;
using System.Collections;

public class Furnishing : MonoBehaviour
{
    private static Transform hologramParent;

    [SerializeField]
    private Hologram prefab;
    public Hologram Prefab { get { return prefab; } }

    private SpriteRenderer spriteRenderer;
    private float targetAlpha = 0;

    public void Awake()
    {
        hologramParent = GameObject.FindGameObjectWithTag("Hologram").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate()
    {
        Color temp = spriteRenderer.color;
        temp.a = Mathf.Lerp(temp.a, targetAlpha, Time.fixedDeltaTime * 2);
        spriteRenderer.color = temp;
    }

    public void SetTargetAlpha(float alpha)
    {
        targetAlpha = alpha;
    }

    /// <summary>
    /// Closes the furnishing menu and creates a hologram corresponding to the selected furnishing.
    /// </summary>
    public void Select()
    {
        FurnishingMenu.Instance.CloseMenu();
        Instantiate(prefab, Camera.main.transform.position + Camera.main.transform.forward * 5 + 
            Camera.main.transform.up * -1, Quaternion.identity).transform.SetParent(hologramParent);
        CostDisplay.Instance.UpdateCost(prefab.Cost);
    }
}
