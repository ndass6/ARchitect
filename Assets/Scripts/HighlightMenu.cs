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
            SetPosition(Selected.transform.position);
    }

    public void OpenMenu(Hologram hologram)
    {
        // Turn on highlight effect
        Selected = hologram;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void CloseMenu()
    {
        CurrentState = State.Idle;

        // Turn off highlight effect
        Selected = null;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
