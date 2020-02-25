using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraOriginal;
    public Transform[] cameraDestinations;

    public bool doTravelCamera = false;
    public float cameraChangeSpeed = 10.0f;
    public float smooth = 0.8f;

    private int travel = 0;
    private Transform currentDestination;


    void Start()
    {
        InvokeRepeating("TravelCamera", cameraChangeSpeed, cameraChangeSpeed);
    }

    void TravelCamera()
    {
        if (!doTravelCamera) return;
        currentDestination = cameraDestinations[Random.Range(0, cameraDestinations.Length)];
        // 0 is delay / start
        // 1 is destination
        // 2 is delay / start
        // 3 is new destination
        // 4 is delay / start
        if (travel == 3) travel = 0;
        travel++;
    }

    void Update()
    {
        if (doTravelCamera)
        {
            LerpCameras();
        }
        else
        {
            travel = 0;
        }
    }

    private void LerpCameras()
    {
        if (travel == 1 || travel == 3)
        {
            transform.position = Vector3.Lerp(transform.position, currentDestination.position, Time.deltaTime * smooth);
            transform.rotation = Quaternion.Slerp(transform.rotation, currentDestination.rotation, Time.deltaTime * smooth);
        }
    }

    public void Begin()
    {
        doTravelCamera = true;
    }
}
