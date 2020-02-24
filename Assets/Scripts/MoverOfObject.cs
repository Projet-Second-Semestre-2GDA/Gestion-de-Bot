using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class MoverOfObject : MonoBehaviour
{
    private bool rightIsPressed = false;
    private string tagObject = "InteractiveObject";

    Vector2 deltaMouseMovement;

    private bool objectSelect = false;
    GameObject selectedObject;
    ObjectBehavior selectedScript;

    Vector3 underTheObject;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Right button is " + (rightIsPressed ? "pressed" : "not pressed"));
        if (rightIsPressed && !objectSelect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.collider.CompareTag(tagObject))
                {
                    objectSelect = true;
                    selectedObject = hit.collider.gameObject;
                    selectedScript = hit.collider.GetComponent<ObjectBehavior>();
                    selectedScript.enabled = false;
                    selectedObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    selectedObject.transform.position += new Vector3(0, 10, 0);
                    //MoveObject(hit.collider.gameObject);
                }
            }
        }
        else if(rightIsPressed && objectSelect)
        {
            selectedObject.transform.position += new Vector3(deltaMouseMovement.x, 0, deltaMouseMovement.y) * Time.deltaTime;
            Ray ray = new Ray(selectedObject.transform.position, Vector3.down);
            RaycastHit hit;
            if (!Physics.Raycast(ray,20))
            {
                selectedObject.transform.position -= new Vector3(deltaMouseMovement.x, 0, deltaMouseMovement.y) * Time.deltaTime;
            }
            else if (Physics.Raycast(ray, out hit))
            {
                underTheObject = hit.point;
                float distance = Vector3.Distance(hit.point, selectedObject.transform.position);
                if (distance > 10.1)
                {
                    selectedObject.transform.position -= new Vector3(0, distance - 10, 0);
                }
                else if (distance < 9.9)
                {
                    selectedObject.transform.position += new Vector3(0, 10-distance, 0);
                }
            }
        }
        else if(!rightIsPressed && objectSelect)
        {
            Ray ray = new Ray(selectedObject.transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                float distance = Vector3.Distance(hit.point, selectedObject.transform.position);
                selectedObject.transform.position += new Vector3(0, -distance + ((selectedObject.GetComponent<MeshFilter>().mesh.bounds.size.y/2) * selectedObject.transform.localScale.y),0);
            }
            selectedScript.enabled = true;
            //Reset des variables
            objectSelect = false;
            selectedObject = null;
            selectedScript = null;
            
        }
        

    }
    private void LateUpdate()
    {
        //rightIsPressed = false;
    }
    public void OnActionRight(InputAction.CallbackContext context)
    {
        rightIsPressed = context.phase == InputActionPhase.Performed;
    }

    public void MouseMovement(InputAction.CallbackContext context)
    {
        deltaMouseMovement = context.ReadValue<Vector2>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (objectSelect)
        {
            Gizmos.DrawSphere(underTheObject, 1);
        }
    }
}
