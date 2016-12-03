using UnityEngine;

public class SmoothBehavior : MonoBehaviour
{
    [SerializeField]
    private bool maintainDistance = false;
    [SerializeField]
    private bool faceCamera = false;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private Vector3 offset;

    public void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;

        offset = transform.position - Camera.main.transform.position;
    }

    public virtual void FixedUpdate()
    {
        if(maintainDistance)
            targetPosition = Camera.main.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * 4);
        
        if(faceCamera)
            targetRotation = Quaternion.LookRotation(Vector3.forward, Camera.main.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 4);
    }

    /// <summary>
    /// Simple wrapper method to move the hologram
    /// </summary>
    /// <param name="value">Vector to move the hologram</param>
    public void Move(Vector3 value)
    {
        targetPosition += value;
    }

    /// <summary>
    /// Simple wrapper method to rotate the hologram
    /// </summary>
    /// <param name="degrees">The angle of increase in degrees</param>
    public void Rotate(float degrees)
    {
        targetRotation *= Quaternion.Euler(new Vector3(0, 0, degrees));
    }
}
