using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraOriginal;
    public Transform[] cameraDestinations;

    public bool doTravelCamera = true;
    public float cameraChangeSpeed = 10.0f;
    public float smooth = 0.8f;

    private float time;
    private int travel = 0;
    private Transform currentDestination;

    void Start()
    {
        InvokeRepeating("TravelCamera", cameraChangeSpeed, cameraChangeSpeed);
    }

    void TravelCamera()
    {
        currentDestination = cameraDestinations[Random.Range(0, cameraDestinations.Length)];
        // 0 is delay / start
        // 1 is destination
        // 2 is delay / start
        // 3 is Starting pos
        // 4 is delay / start
        if (travel == 4) travel = 0;
        travel++;
    }

    void Update()
    {
        if (travel == 1)
        {
            transform.position = Vector3.Lerp(transform.position, currentDestination.position, Time.deltaTime * smooth);
            transform.rotation = Quaternion.Slerp(transform.rotation, currentDestination.rotation, Time.deltaTime * smooth);
        }
        if (travel == 3)
        {
            transform.position = Vector3.Lerp(transform.position, cameraOriginal.position, Time.deltaTime * smooth);
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraOriginal.rotation, Time.deltaTime * smooth);
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AnimateCamera : MonoBehaviour
// {

//     public Transform cameraDestination;
//     public Transform cameraOriginal;

//     public float cameraChangeSpeed = 10.0f;
//     public float smooth = 0.8f;

//     private float time;
//     private int travel = 0;

//     void Start()
//     {
//         InvokeRepeating("TravelCamera", cameraChangeSpeed, cameraChangeSpeed);
//     }

//     void TravelCamera()
//     {
//         // 0 is delay / start
//         // 1 is destination
//         // 2 is delay / start
//         // 3 is Starting pos
//         // 4 is delay / start
//         if (travel == 4) travel = 0;
//         travel++;
//     }

//     void Update()
//     {
//         if (travel == 1)
//         {
//             transform.position = Vector3.Lerp(transform.position, cameraDestination.position, Time.deltaTime * smooth);
//             transform.rotation = Quaternion.Slerp(transform.rotation, cameraDestination.rotation, Time.deltaTime * smooth);
//         }
//         if (travel == 3)
//         {
//             transform.position = Vector3.Lerp(transform.position, cameraOriginal.position, Time.deltaTime * smooth);
//             transform.rotation = Quaternion.Slerp(transform.rotation, cameraOriginal.rotation, Time.deltaTime * smooth);
//         }
//     }
// }

