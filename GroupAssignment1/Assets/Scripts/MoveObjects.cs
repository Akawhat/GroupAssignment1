using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    Command command;

    bool mouseMovement;
    Vector3 offScreenpos;
    Vector3 offsetValue;

    GameObject moveableObj;
    void Start()
    {
        command = GetComponent<Command>();
    }

    GameObject ReturnClickedObject(out RaycastHit hit) // allows for objects to be cleared once the object has been clicked on 
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // when the left mouse button is pressed down 
        {
            RaycastHit hitInfo;
            moveableObj = ReturnClickedObject(out hitInfo);
            if (moveableObj != null)
            {
                GameObject temp;
                temp = moveableObj;

                if (moveableObj.tag == "Moveable")
                {
                    mouseMovement = true;
                    offScreenpos = Camera.main.WorldToScreenPoint(moveableObj.transform.position);
                    offsetValue = moveableObj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, offScreenpos.z));
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseMovement = false;
        }

        if (Input.GetMouseButtonDown(1)) // when the right mouse button is pressed down 
        {
            RaycastHit hitInfo;
            moveableObj = ReturnClickedObject(out hitInfo);
            if (moveableObj != null)
            {
                if (moveableObj.tag == "Moveable")
                {
                    Destroy(moveableObj);
                }
            }
        }

        if (mouseMovement) // checks for movemnt of mouse 
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, offScreenpos.z); // uses a vector 3 to track the mouse position on screen 
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offsetValue; // after tracking the mouse position from screen, this converts the position of the screen to the to the offseted world position 
            moveableObj.transform.position = currentPosition; // update of the games object postion to create a moveable gameobjct 
        }


    }

}
