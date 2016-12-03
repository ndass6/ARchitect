using UnityEngine;
using System.Collections;

public class HighlightMenu : SmoothBehaviour
{
    [HideInInspector]
    public static HighlightMenu Instance;

    public Hologram Selected;

    public enum State { Idle, Move, Rotate }
    public State CurrentState = State.Idle;

    public void Awake()
    {
        Instance = this;
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
        CurrentState = State.Idle;

        // Failsafe
        if(!hologram || hologram.Equals(Selected))
        {
            CloseMenu();
            return;
        }

        // TODO Turn on highlight effect
        Selected = hologram;
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        // TODO Turn off highlight effect
        Selected = null;
        gameObject.SetActive(false);
    }
}
