using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCharacter : MonoBehaviour
{
    private bool isDragged = false;
    private Vector3 offset;

    private void Update()
    {
        if (isDragged) 
        {
            transform.position = VectorClamp(Camera.main.ScreenToWorldPoint(Input.mousePosition), 5.18f, 5.58f);
        }

    }

    private void OnMouseDown()
    {
        offset = new Vector3(transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 2f, transform.position.z - Camera.main.ScreenToWorldPoint(Input.mousePosition).z);

        InputManager.instance.setDragged(true);
        isDragged = true;
    }

    private void OnMouseUp()
    {
        InputManager.instance.setDragged(false);
        isDragged = false;
    }

    private Vector3 VectorClamp(Vector3 vector, float minX, float maxX)
    {
        return new Vector3(Mathf.Clamp(vector.x + offset.x, -minX, maxX) , 2f, vector.z + offset.z);
    }
}
