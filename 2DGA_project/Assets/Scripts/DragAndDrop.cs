using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject selectedObject;
    Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            Collider2D colliderTarget = Physics2D.OverlapPoint(mousePosition);
            if (colliderTarget) {
                selectedObject = colliderTarget.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }

        if (selectedObject) {
            selectedObject.transform.position = mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }
}
