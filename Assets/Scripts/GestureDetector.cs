using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureDetector : MonoBehaviour
{
    public static GestureDetector Instance { get; private set; }

    public GestureRecognizer ActiveRecognizer { get; private set; }
    public GestureRecognizer TapRecognizer { get; private set; }
    public GestureRecognizer NavigationRecognizer { get; private set; }
    public GestureRecognizer ManipulationRecognizer { get; private set; }

    public bool IsNavigating { get; private set; }
    public bool IsManipulating { get; private set; }

    private Vector3 navigationPreviousRotation { get; set; }
    private Vector3 manipulationPreviousPosition { get; set; }

    private float rotationFactor = 5.0f;
    private float translationFactor = 5.0f;

    public void Awake()
    {
        Instance = this;

        TapRecognizer = new GestureRecognizer();
        TapRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.Hold);
        TapRecognizer.TappedEvent += Recognizer_TappedEvent;
        TapRecognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        
        NavigationRecognizer = new GestureRecognizer();
        NavigationRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.Hold
            | GestureSettings.NavigationX);
        NavigationRecognizer.TappedEvent += Recognizer_TappedEvent;
        NavigationRecognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        NavigationRecognizer.NavigationStartedEvent += Recognizer_NavigationStartedEvent;
        NavigationRecognizer.NavigationUpdatedEvent += Recognizer_NavigationUpdatedEvent;
        NavigationRecognizer.NavigationCompletedEvent += Recognizer_NavigationCompletedEvent;
        NavigationRecognizer.NavigationCanceledEvent += Recognizer_NavigationCanceledEvent;

        ManipulationRecognizer = new GestureRecognizer();
        ManipulationRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.Hold
            | GestureSettings.ManipulationTranslate);
        ManipulationRecognizer.TappedEvent += Recognizer_TappedEvent;
        ManipulationRecognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        ManipulationRecognizer.ManipulationStartedEvent += Recognizer_ManipulationStartedEvent;
        ManipulationRecognizer.ManipulationUpdatedEvent += Recognizer_ManipulationUpdatedEvent;
        ManipulationRecognizer.ManipulationCompletedEvent += Recognizer_ManipulationCompletedEvent;
        ManipulationRecognizer.ManipulationCanceledEvent += Recognizer_ManipulationCanceledEvent;

        ResetGestureRecognizers();
    }

    void OnDestroy()
    {
        TapRecognizer.TappedEvent -= Recognizer_TappedEvent;
        TapRecognizer.HoldStartedEvent -= Recognizer_HoldStartedEvent;

        NavigationRecognizer.TappedEvent -= Recognizer_TappedEvent;
        NavigationRecognizer.HoldStartedEvent -= Recognizer_HoldStartedEvent;
        NavigationRecognizer.NavigationStartedEvent -= Recognizer_NavigationStartedEvent;
        NavigationRecognizer.NavigationUpdatedEvent -= Recognizer_NavigationUpdatedEvent;
        NavigationRecognizer.NavigationCompletedEvent -= Recognizer_NavigationCompletedEvent;
        NavigationRecognizer.NavigationCanceledEvent -= Recognizer_NavigationCanceledEvent;

        ManipulationRecognizer.TappedEvent -= Recognizer_TappedEvent;
        ManipulationRecognizer.HoldStartedEvent -= Recognizer_HoldStartedEvent;
        ManipulationRecognizer.ManipulationStartedEvent -= Recognizer_ManipulationStartedEvent;
        ManipulationRecognizer.ManipulationUpdatedEvent -= Recognizer_ManipulationUpdatedEvent;
        ManipulationRecognizer.ManipulationCompletedEvent -= Recognizer_ManipulationCompletedEvent;
        ManipulationRecognizer.ManipulationCanceledEvent -= Recognizer_ManipulationCanceledEvent;

        ActiveRecognizer.StopCapturingGestures();
    }

    public void ResetGestureRecognizers()
    {
        Transition(TapRecognizer);
    }

    public void Transition(GestureRecognizer newRecognizer)
    {
        if (newRecognizer == null)
        {
            return;
        }
        if (ActiveRecognizer != null)
        {
            if (ActiveRecognizer == newRecognizer)
            {
                return;
            }
            ActiveRecognizer.CancelGestures();
            ActiveRecognizer.StopCapturingGestures();
        }
        newRecognizer.StartCapturingGestures();
        ActiveRecognizer = newRecognizer;
    }

    private void Recognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = true;
        navigationPreviousRotation = relativePosition;
    }

    private void Recognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {

        // Make sure an object is selected
        if (HighlightMenu.Instance.Selected)
        {
            // Make sure the current state is rotate
            if (HighlightMenu.Instance.CurrentState == HighlightMenu.State.Rotate)
            {
                // Rotate along the Y axis using rotationFactor

                Vector3 diffVector = relativePosition - navigationPreviousRotation;
                HighlightMenu.Instance.Selected.AddRotation(-1 * diffVector.x * rotationFactor);
                navigationPreviousRotation = relativePosition;
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
        manipulationPreviousPosition = position;
    }

    private void Recognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        // Make sure an object is selected
        if (HighlightMenu.Instance.Selected)
        {
            // Make sure the current state is move
            if (HighlightMenu.Instance.CurrentState == HighlightMenu.State.Move)
            {
                Vector3 diffVector = position - manipulationPreviousPosition;
                HighlightMenu.Instance.Selected.AddPosition(diffVector * translationFactor);
                manipulationPreviousPosition = position;
            }
        }
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
            Transition(TapRecognizer);
        }
        else if (hitInfo.collider.GetComponent<HighlightButton>())
        {
            switch (hitInfo.collider.GetComponent<HighlightButton>().CurrentState)
            {
                case HighlightButton.State.Move:
                    HighlightMenu.Instance.CurrentState = HighlightMenu.State.Move;
                    Transition(ManipulationRecognizer);
                    break;
                case HighlightButton.State.Rotate:
                    HighlightMenu.Instance.CurrentState = HighlightMenu.State.Rotate;
                    Transition(NavigationRecognizer);
                    break;
                case HighlightButton.State.Delete:
                    Destroy(HighlightMenu.Instance.Selected.gameObject);
                    CostDisplay.Instance.UpdateCost();
                    Transition(TapRecognizer);
                    break;
            }
        }
        else if (hitInfo.collider.GetComponent<Furnishing>())
        {
            hitInfo.collider.GetComponent<Furnishing>().Select();
            Transition(TapRecognizer);
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