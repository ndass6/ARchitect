using System;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private TextMesh text;

    private SpriteRenderer spriteRenderer;
    private float targetAlpha = 0;

    public void Awake()
    {
        text = GetComponentInChildren<TextMesh>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        RaycastHit hitInfo;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo);
        if(hitInfo.collider && hitInfo.collider.GetComponent<Furnishing>())
        {
            Furnishing obj = hitInfo.collider.GetComponent<Furnishing>();
            text.text = "<b>" + obj.name.Substring(0, obj.name.Length - 7) + ":</b>\n<i>$" + obj.Prefab.Cost.ToString("F2") + "</i>";
            targetAlpha = 1;
        }
        else
        {
            text.text = "";
            targetAlpha = 0;
        }
    }

    public void FixedUpdate()
    {
        Color temp = spriteRenderer.color;
        temp.a = Mathf.Lerp(temp.a, targetAlpha, Time.fixedDeltaTime);
        spriteRenderer.color = temp;
    }
}
