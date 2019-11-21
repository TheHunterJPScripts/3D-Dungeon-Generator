using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisualization : MonoBehaviour
{
    public bool enable = true;
    float size = 0.5f;
    Color enableColor = Color.green;
    Color disableColor = Color.red;

    private void OnDrawGizmos() {
        if (!enable)
            return;

        Gizmos.color = enable == true ? enableColor : disableColor;
        Gizmos.DrawSphere(transform.position, size);
    }
}
