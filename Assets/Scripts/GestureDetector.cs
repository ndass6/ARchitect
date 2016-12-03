using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureDetector : MonoBehaviour
{
    public static GestureDetector Instance { get; private set; }

    public GestureRecognizer Recognizer { get; private set; }

    public bool IsNavigating { get; private set; }

    public Vector3 NavigationPosition { get; private set; }

    public bool IsManipulating { get; private set; }

    public Vector3 ManipulationPosition { get; private set; }

    public void Awake()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Tap gestures
        Recognizer = new GestureRecognizer();

        // Instantiate the NavigationRecognizer.
        Recognizer = new GestureRecognizer();

        // Add RecognizableGestures.
        Recognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.Hold
            | GestureSettings.NavigationX | GestureSettings.ManipulationTranslate);

        Recognizer.TappedEvent += Recognizer_TappedEvent;
        Recognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        Recognizer.NavigationStartedEvent += Recognizer_NavigationStartedEvent;
        Recognizer.NavigationUpdatedEvent += Recognizer_NavigationUpdatedEvent;
        Recognizer.NavigationCompletedEvent += Recognizer_NavigationCompletedEvent;
        Recognizer.NavigationCanceledEvent += Recognizer_NavigationCanceledEvent;
        Recognizer.ManipulationStartedEvent += Recognizer_ManipulationStartedEvent;
        Recognizer.ManipulationUpdatedEvent += Recognizer_ManipulationUpdatedEvent;
        Recognizer.ManipulationCompletedEvent += Recognizer_ManipulationCompletedEvent;
        Recognizer.ManipulationCanceledEvent += Recognizer_ManipulationCanceledEvent;

        Recognizer.StartCapturingGestures();
    }

    void OnDestroy()
    {
        Recognizer.TappedEvent -= Recognizer_TappedEvent;
        Recognizer.HoldStartedEvent -= Recognizer_HoldStartedEvent;

        Recognizer.NavigationStartedEvent -= Recognizer_NavigationStartedEvent;
        Recognizer.NavigationUpdatedEvent -= Recognizer_NavigationUpdatedEvent;
        Recognizer.NavigationCompletedEvent -= Recognizer_NavigationCompletedEvent;
        Recognizer.NavigationCanceledEvent -= Recognizer_NavigationCanceledEvent;

        Recognizer.ManipulationStartedEvent -= Recognizer_ManipulationStartedEvent;
        Recognizer.ManipulationUpdatedEvent -= Recognizer_ManipulationUpdatedEvent;
        Recognizer.ManipulationCompletedEvent -= Recognizer_ManipulationCompletedEvent;
        Recognizer.ManipulationCanceledEvent -= Recognizer_ManipulationCanceledEvent;

        Recognizer.StopCapturingGestures();
    }

    private void Recognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = true;
        NavigationPosition = relativePosition;
    }

    private void Recognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = true;
        NavigationPosition = relativePosition;

        // Make sure an object is selected
        if (HighlightMenu.Instance.Selected)
        {
            // Make sure the current state is rotate
            if (HighlightMenu.Instance.CurrentState == HighlightMenu.State.Rotate)
            {
                // Calculate rotationFactor based on GestureManager's NavigationPosition.X and multiply by RotationSensitivity.
                // This will help control the amount of rotation.
                float rotationFactor = Instance.NavigationPosition.x * 10.0f;

                //transform.Rotate along the Y axis using rotationFactor
                transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
            }
        }
    }

    private void Recognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = false;
    }

    private void Recognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = false;
    }

    private void Recognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = true;
        ManipulationPosition = position;
    }

    private void Recognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = true;
        ManipulationPosition = position;
    }

    private void Recognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = false;
    }

    private void Recognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = false;
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
    {
        RaycastHit hitInfo;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo);

        // Perform action based on object clicked
        if (hitInfo.collider.GetComponent<Hologram>())
        {
            HighlightMenu.Instance.OpenMenu(hitInfo.collider.GetComponent<Hologram>());
        }
        else if (hitInfo.collider.GetComponent<HighlightButton>())
        {
            switch (hitInfo.collider.GetComponent<HighlightButton>().CurrentState)
            {
                case HighlightButton.State.Move:
                    HighlightMenu.Instance.CurrentState = HighlightMenu.State.Move;
                    break;
                case HighlightButton.State.Rotate:
                    HighlightMenu.Instance.CurrentState = HighlightMenu.State.Rotate;
                    break;
                case HighlightButton.State.Delete:
                    Destroy(HighlightMenu.Instance.Selected.gameObject);
                    CostDisplay.Instance.UpdateCost();
                    break;
            }
        }
        else if (hitInfo.collider.GetComponent<Furnishing>())
        {
            hitInfo.collider.GetComponent<Furnishing>().Select();
        }
    }

    private void Recognizer_HoldStartedEvent(InteractionSourceKind source, Ray headRay)
    {
        if (FurnishingMenu.Instance.gameObject.activeSelf)
            FurnishingMenu.Instance.CloseMenu();
        else
            FurnishingMenu.Instance.OpenMenu();
    }
}