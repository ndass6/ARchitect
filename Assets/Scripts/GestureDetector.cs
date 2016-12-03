using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureDetector : MonoBehaviour
{
    public static GestureDetector Instance { get; private set; }

    GestureRecognizer recognizer;
    
    public void Awake()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();

        // Executed when a tap event is detected
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Raycast calculations
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            // Raycast hits an object
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                Collider col = hitInfo.collider;
            }

            // Raycast does not hit an object
            else
            {

            }
        };

        recognizer.StartCapturingGestures();
    }
}