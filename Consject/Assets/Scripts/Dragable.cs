using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{

    Vector3 MousePositionOffset;

    private Vector3 getMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseDown()
    {
        MousePositionOffset = gameObject.transform.position - getMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = getMouseWorldPosition() + MousePositionOffset;
    }
}
