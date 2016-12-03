using UnityEngine;
using System.Collections;

public class HighlightButton : MonoBehaviour
{
    public enum State { Move, Rotate, Delete }
    public State CurrentState;
}
