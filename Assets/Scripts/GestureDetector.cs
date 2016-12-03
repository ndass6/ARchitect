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
<<<<<<< HEAD
=======
                if (col.tag == "Card")
                {
                    // GetComponent<CardController>().RemoveCard(col.GetComponent<Card>());
                    return;
                }
>>>>>>> origin/master
            }

            // Raycast does not hit an object
            else
            {
<<<<<<< HEAD

=======
                // TODO Call photo capture instead
                // gameObject.AddComponent<CameraCapture>();
>>>>>>> origin/master
            }
        };

        recognizer.StartCapturingGestures();
    }
}