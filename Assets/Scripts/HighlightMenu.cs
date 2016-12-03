using UnityEngine;
using System.Collections;

public class HighlightMenu : SmoothBehaviour
{
    [HideInInspector]
    public static HighlightMenu Instance;

    public Hologram Selected;

    public enum State { Idle, Move, Rotate }
    public State CurrentState = State.Idle;

    private bool busy;

    private SpriteRenderer menuRenderer;
    private SpriteRenderer moveRenderer;
    private SpriteRenderer rotateRenderer;
    private SpriteRenderer deleteRenderer;

    public void Awake()
    {
        Instance = this;

        menuRenderer = GetComponent<SpriteRenderer>();
        moveRenderer = transform.FindChild("Move Button").GetComponent<SpriteRenderer>();
        rotateRenderer = transform.FindChild("Rotate Button").GetComponent<SpriteRenderer>();
        deleteRenderer = transform.FindChild("Delete Button").GetComponent<SpriteRenderer>();
    }

    public override void Start()
    {
        base.Start();
        CloseMenu();
    }

    public void Update()
    {
        if(Selected)
            SetPosition(Selected.transform.position + new Vector3(0, 2, 0));
    }

    public void OpenMenu(Hologram hologram)
    {
        if(!busy)
        {
            CurrentState = State.Idle;

            if(hologram.Equals(Selected))
            {
                CloseMenu();
                return;
            }
            else if(FurnishingMenu.Instance.gameObject.activeSelf)
            {
                FurnishingMenu.Instance.CloseMenu();
            }

            // TODO Turn on highlight effect
            Selected = hologram;
            transform.position = targetPosition = Selected.transform.position + new Vector3(0, 2, 0);
            transform.rotation = targetRotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
            gameObject.SetActive(true);
            StartCoroutine(Appear());
        }
    }

    public void CloseMenu()
    {
        if(!busy)
        {
            // TODO Turn off highlight effect
            Selected = null;
            StartCoroutine(Disappear());
        }
    }

    /// <summary>
    /// Coroutine to display the menu.
    /// </summary>
    private IEnumerator Appear()
    {
        busy = true;

        Vector3 backgroundScale = new Vector3(0, 0.05f, 1);

        Color panelColor = menuRenderer.color;
        Color buttonColor = moveRenderer.color;

        for(float x = backgroundScale.x; x <= 1f; x = Mathf.Lerp(x, 1.01f, 0.1f))
        {
            panelColor.a = x / 2;
            menuRenderer.color = panelColor;

            backgroundScale.x = x;
            transform.localScale = backgroundScale;

            yield return new WaitForSeconds(0.005f);
        }
        for(float y = backgroundScale.y; y <= 1f; y = Mathf.Lerp(y, 1.01f, 0.1f))
        {
            panelColor.a = 0.5f + y / 2;
            menuRenderer.color = panelColor;

            backgroundScale.y = y;
            transform.localScale = backgroundScale;

            yield return new WaitForSeconds(0.005f);
        }
        for(float a = buttonColor.a; a <= 1f; a += 0.05f)
        {
            buttonColor.a = a;
            moveRenderer.color = buttonColor;
            rotateRenderer.color = buttonColor;
            deleteRenderer.color = buttonColor;

            yield return new WaitForSeconds(0.005f);
        }

        busy = false;
    }

    /// <summary>
    /// Coroutine to make the menu disappear.
    /// </summary>
    private IEnumerator Disappear()
    {
        busy = true;

        Color buttonColor = moveRenderer.color;
        Vector3 backgroundScale = transform.localScale;

        for(float a = buttonColor.a; a >= 0f; a -= 0.05f)
        {
            buttonColor.a = a;
            moveRenderer.color = buttonColor;
            rotateRenderer.color = buttonColor;
            deleteRenderer.color = buttonColor;

            yield return new WaitForSeconds(0.005f);
        }
        for(float y = backgroundScale.y; y >= 0.05f; y = Mathf.Lerp(y, 0.04f, 0.1f))
        {
            backgroundScale.y = y;
            transform.localScale = backgroundScale;

            yield return new WaitForSeconds(0.005f);
        }
        for(float x = backgroundScale.x; x >= 0f; x = Mathf.Lerp(x, -0.01f, 0.1f))
        {
            backgroundScale.x = x;
            transform.localScale = backgroundScale;

            yield return new WaitForSeconds(0.005f);
        }

        gameObject.SetActive(false);
        busy = false;
    }
}
