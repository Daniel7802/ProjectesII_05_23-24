using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    // CAMERA ZOOM VARIABLES
    private float startZoom;
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 4f;
    private float maxZoom = 8f;
    private float velocity = 0f;
    private float smoothTimeGo = 0.25f;
    private float smoothTimeReturn = 1f;

    [SerializeField]
    private GameObject boomerang;

    private BoomerangThrow bt;

    // CAMERA FOLLOW VARIABLES
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 cameraVelocity = Vector3.zero;

    [SerializeField]
    private Transform cameraTarget;

    // Start is called before the first frame update
    void Start()
    {
        startZoom = cam.orthographicSize;
        zoom = startZoom;
        bt = boomerang.GetComponent<BoomerangThrow>();
    }

    // Update is called once per frame
    void Update()
    {
        // CAMERA ZOOM CODE
        if(bt.mouseHold)
        {
            zoom -= 1 * zoomMultiplier * Time.deltaTime;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTimeGo);
        }
        else if(!bt.mouseHold)
        {
            zoom += 1 * zoomMultiplier * 5 * Time.deltaTime;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(zoom, startZoom, ref velocity, smoothTimeReturn);

            //cam.orthographicSize = startZoom;
        }

        //CAMERA FOLLOW CODE
        Vector3 targetPosition = cameraTarget.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, smoothTimeGo);
    }
}
