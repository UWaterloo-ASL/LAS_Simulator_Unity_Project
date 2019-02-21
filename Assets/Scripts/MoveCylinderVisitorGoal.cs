using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCylinderVisitorGoal : MonoBehaviour
{
    public Camera camera;

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        Debug.Log("click on Goal");
        screenPoint = camera.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = camera.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
