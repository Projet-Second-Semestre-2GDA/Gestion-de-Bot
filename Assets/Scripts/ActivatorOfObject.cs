using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class ActivatorOfObject : MonoBehaviour
{
    private bool leftIsPressed = false;
    private string tagObject = "InteractiveObject";


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Left button is " + (leftIsPressed ? "pressed" : "not pressed"));

        if (leftIsPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.collider.CompareTag(tagObject))
                {
                    var objectTouch = hit.collider.gameObject.GetComponent<ObjectBehavior>();
                    ActionTheObject(objectTouch);
                }
            }
        }
        
    }
    private void LateUpdate()
    {
        leftIsPressed = false;
    }
    public void OnActionLeft(InputAction.CallbackContext context)
    {
        leftIsPressed = context.phase == InputActionPhase.Performed;
    }

    private void ActionTheObject(ObjectBehavior objectToAction)
    {
        objectToAction.Action();
    }
}
