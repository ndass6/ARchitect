using UnityEngine;
using System.Collections;

public class FurnishingMenu : SmoothBehaviour
{
    [HideInInspector]
    public static FurnishingMenu Instance;

    public void Awake()
    {
        Instance = this;
    }

    public override void Start()
    {
        base.Start();
        CloseMenu();
    }

    public void OpenMenu()
    {
        if(HighlightMenu.Instance.Selected)
            HighlightMenu.Instance.CloseMenu();
        
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
