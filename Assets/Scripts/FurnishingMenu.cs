using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FurnishingMenu : SmoothBehaviour
{
    [HideInInspector]
    public static FurnishingMenu Instance;

    public List<Furnishing> Furnishings;

    private bool busy;

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
        if(!busy)
        {
            if(HighlightMenu.Instance.Selected)
                HighlightMenu.Instance.CloseMenu();

            transform.position = targetPosition = Camera.main.transform.position + Camera.main.transform.forward * 20;
            transform.rotation = targetRotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
            gameObject.SetActive(true);
            StartCoroutine(Appear());
        }
    }

    public void CloseMenu()
    {
        if(!busy)
            StartCoroutine(Disappear());
    }

    /// <summary>
    /// Coroutine to display the menu.
    /// </summary>
    private IEnumerator Appear()
    {
        busy = true;

        Color temp;
        foreach(Furnishing furnishing in Furnishings)
        {
            temp = furnishing.GetComponent<SpriteRenderer>().color;
            temp.a = 0;
            furnishing.GetComponent<SpriteRenderer>().color = temp;
        }
        foreach(Furnishing furnishing in Furnishings)
        {
            furnishing.SetTargetAlpha(1);
            yield return new WaitForSeconds(0.05f);
        }

        busy = false;
    }

    /// <summary>
    /// Coroutine to make the menu disappear.
    /// </summary>
    private IEnumerator Disappear()
    {
        busy = true;

        foreach(Furnishing furnishing in Furnishings)
        {
            furnishing.SetTargetAlpha(0);
        }

        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        busy = false;
    }
}
