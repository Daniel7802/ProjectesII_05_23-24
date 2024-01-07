using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    // CAMERA ZOOM VARIABLES
    private float startTimer = 0.25f;

    private float startZoom;
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 4f;
    private float maxZoom = 8f;
    private float velocity = 0f;
    private float smoothTimeGo = 0.25f;
    private float smoothTimeReturn = 1f;

    private Vector2 clickDirection;
    private Vector2 playerPosition, finalPoint;
    private Vector2 cameraMovementVector;

    private float movementTimer = 0.50f;

    private float cameraOffset = 2f;

    [SerializeField]
    private GameObject boomerang;

    [SerializeField]
    private GameObject shadowBoomerang;

    private BoomerangThrow bt;

    private BoomerangThrow sbt;

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
        sbt = shadowBoomerang.GetComponent<BoomerangThrow>();
    }

    // Update is called once per frame
    void Update()
    {

        clickDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = cameraTarget.transform.position;

        cameraMovementVector = clickDirection - playerPosition;
        cameraMovementVector.Normalize();

        finalPoint = (cameraMovementVector * cameraOffset) + playerPosition;

        // CAMERA ZOOM CODE
        if (bt.mouseHold || sbt.mouseHold)
        {
            startTimer -= Time.deltaTime;

            if(startTimer < 0)
            {
                if (movementTimer > 0)
                {
                    zoom -= 1 * zoomMultiplier * Time.deltaTime;
                    zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
                    cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTimeGo);
                    movementTimer -= Time.deltaTime;
                }
            }

            Vector3 vel = new Vector3(0, 0, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, finalPoint, ref vel, 0.03f);
        }
        else if(!bt.mouseHold || !sbt.mouseHold)
        {
            zoom += 1 * zoomMultiplier * 5 * Time.deltaTime;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(zoom, startZoom, ref velocity, smoothTimeReturn);
            startTimer = 0.25f;
           //cam.orthographicSize = startZoom;
        }

        //CAMERA FOLLOW CODE
        if (!bt.mouseHold && !sbt.mouseHold)
        {
            Vector3 targetPosition = cameraTarget.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, smoothTimeGo);

            movementTimer = 0.5f;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(clickDirection, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(playerPosition, 2.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(finalPoint, 2.1f);
    }
}
