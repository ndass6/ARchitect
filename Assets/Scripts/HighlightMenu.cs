using UnityEngine;
using System.Collections;

public class HighlightMenu : SmoothBehaviour
{
    public Hologram Selected;
    public enum State { Idle, Move, Rotate }

    [SerializeField]
    private State state = State.Idle;

    public override void Start()
    {
        base.Start();
        CloseMenu();
    }

    public void Update()
    {
        // Follow selected hologram if not null
    }

    public void OpenMenu(Hologram hologram)
    {
        // Turn on highlight effect
        Selected = hologram;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void CloseMenu()
    {
        state = State.Idle;

        // Turn off highlight effect
        Selected = null;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
