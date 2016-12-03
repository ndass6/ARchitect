using UnityEngine;
using System.Collections;

public class Hologram : SmoothBehavior
{
    public enum State { Idle, Move, Rotate }

    [SerializeField]
    private State state = State.Idle;
    [SerializeField]
    private float cost = 0;

    public void OpenMenu()
    {
        // Set highlight menu selected object to this
        // Show highlight menu
        // Turn on highlight effect
    }

    public void CloseMenu()
    {
        state = State.Idle;

        // Set highlight menu selected object to null
        // Hide hightlight menu
        // Turn off highlight effect
    }
}
