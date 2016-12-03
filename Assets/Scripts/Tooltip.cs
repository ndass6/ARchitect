using System;
using Unity;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    void Update() {
        RaycastHit hitInfo;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo);
        if (hitInfo.collider.GetComponent<Furnishing>())
        {
            showTooltip(hitInfo.collider.gameObject);
        }
        else
        {
            showTooltip(null);
        }
    }

    private void showTooltip(GameObject obj)
    {

    }
}
