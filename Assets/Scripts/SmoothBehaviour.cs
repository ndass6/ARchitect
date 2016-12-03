using UnityEngine;

public class SmoothBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool maintainDistance = false;
    [SerializeField]
    private bool faceCamera = false;

    [SerializeField]
    private bool smoothPosition = false;
    [SerializeField]
    private bool smoothRotation = false;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private Vector3 offset;

    public virtual void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;

        offset = transform.position - Camera.main.transform.position;
    }

    public virtual void FixedUpdate()
    {
        if(maintainDistance)
            targetPosition = Camera.main.transform.position + offset;
        transform.position = smoothPosition ? Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * 4) : targetPosition;

        if(faceCamera)
            targetRotation = Quaternion.LookRotation(Vector3.forward, Camera.main.transform.position - transform.position);
        transform.rotation = smoothRotation ? Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 4) : targetRotation;
    }

    /// <summary>
    /// Simple wrapper method to move the hologram.
    /// </summary>
    /// <param name="value">Vector to move the hologram</param>
    public void AddPosition(Vector3 value)
    {
        targetPosition += value;
    }

    /// <summary>
    /// Simple wrapper method to set the hologram's position.
    /// </summary>
    /// <param name="value">Vector to set the hologram to</param>
    public void SetPosition(Vector3 value)
    {
        targetPosition = value;
    }

    /// <summary>
    /// Simple wrapper method to rotate the hologram.
    /// </summary>
    /// <param name="degrees">The angle of increase in degrees</param>
    public void AddRotation(float degrees)
    {
        targetRotation *= Quaternion.Euler(new Vector3(0, 0, degrees));
    }

    /// <summary>
    /// Simple wrapper method to set the hologram's rotation.
    /// </summary>
    /// <param name="degrees">The angle to set the hologram's rotation to</param>
    public void SetRotation(float degrees)
    {
        targetRotation = Quaternion.Euler(new Vector3(0, 0, degrees));
    }
}
