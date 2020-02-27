using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatonicParentController : MonoBehaviour
{
    private int numActivePlatonics = 0;
    private PlatonicController[] platonicControllers;

    public float spinRate = 10f;
    public int NumActivePlatonics { get => numActivePlatonics; set => numActivePlatonics = value; }

    void Start()
    {
        platonicControllers = GetComponentsInChildren<PlatonicController>();
    }

    void Update()
    {
        Spin();
        UpdateNumPlatonics();
    }

    private void Spin()
    {
        transform.Rotate(0, spinRate * Time.deltaTime, 0);
    }

    private void UpdateNumPlatonics()
    {
        numActivePlatonics = 0;
        foreach (var platonic in platonicControllers)
        {
            if (platonic.IsActive) NumActivePlatonics++;
        }
    }
}
