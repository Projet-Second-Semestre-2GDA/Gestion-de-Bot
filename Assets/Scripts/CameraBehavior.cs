using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private bool inverseZoom = false;
    
    [SerializeField] private float heightMax = 30f; //with the floor
    [SerializeField] private float heightMin = 5f; //with the floor
    [SerializeField] private float speedMovement = 20f; //with the floor
    [SerializeField] private float speedZoom = 10f; //with the floor

    Vector2 cameraMovement;
    Vector2 zoom;

    Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float fixedDeltaTime = Time.fixedDeltaTime;
        Vector3 actualPosition = transform.position;
        Vector3 nextPosition = transform.position;

        //Deplacement de la camera
        nextPosition += (new Vector3(cameraMovement.x,0, cameraMovement.y) * speedMovement * fixedDeltaTime);
        
        //Zoom de la camera
        //nextPosition += (new Vector3(0, -zoom.y, 0) * speedZoom * fixedDeltaTime); //zoom en déplaçant la caméra (strictement vers le bas) (fonctionnel)
        nextPosition += (transform.forward * zoom.y * (inverseZoom? -1:1) * speedZoom * fixedDeltaTime); //zoom en déplaçant la caméra (vers la ou elle regarde) (fonctionnel)


        //camera.fieldOfView = Mathf.Max(((-zoom.y /** speedZoom*/ * fixedDeltaTime) + camera.fieldOfView), 1); //zoom en changeant la FOV, donc hauteur caméra fixe (fonctionnel)
        //mais la caméra se déplace toujours a la meme vitesse du coup


        transform.position = nextPosition;
    }

    public void CameraDeplacement(InputAction.CallbackContext context)
    {
        cameraMovement = context.ReadValue<Vector2>();
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        zoom = context.ReadValue<Vector2>();
    }

}
