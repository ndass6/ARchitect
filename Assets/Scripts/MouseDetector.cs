using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo))
            {
                // Perform action based on object clicked
                if(hitInfo.collider.GetComponent<Hologram>())
                {
                    HighlightMenu.Instance.OpenMenu(hitInfo.collider.GetComponent<Hologram>());
                }
                else if(hitInfo.collider.GetComponent<HighlightButton>())
                {
                    switch(hitInfo.collider.GetComponent<HighlightButton>().CurrentState)
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
                else if(hitInfo.collider.GetComponent<Furnishing>())
                {
                    hitInfo.collider.GetComponent<Furnishing>().Select();
                }
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if(FurnishingMenu.Instance.gameObject.activeSelf)
                FurnishingMenu.Instance.CloseMenu();
            else
                FurnishingMenu.Instance.OpenMenu();
        }
    }
}
