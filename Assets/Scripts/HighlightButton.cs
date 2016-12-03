using UnityEngine;
using System.Collections;

public class HighlightButton : MonoBehaviour
{
    public enum State { Idle, Move, Rotate, Delete }
    public State CurrentState = State.Idle;
}
