using UnityEngine;
using System.Collections;

public class HighlightButton : MonoBehaviour
{
    public enum State { Move, Rotate, Delete }
    public State CurrentState;

    [SerializeField]
    private SpriteRenderer glowRenderer;
    private float glowTargetAlpha = 0;

    public void FixedUpdate()
    {
        Color temp = glowRenderer.color;
        temp.a = Mathf.Lerp(temp.a, glowTargetAlpha, Time.fixedDeltaTime * 4);
        glowRenderer.color = temp;
    }

    public void SetGlowAlpha(float alpha)
    {
        glowTargetAlpha = alpha;
    }
}
