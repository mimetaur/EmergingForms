using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swell : MonoBehaviour
{
    public float swellMin = 0.25f;
    public float swellMax = 1.0f;
    public float swellFactor = 1.0f;
    public float swellOffset = 1.0f;
    private float swell = 1.0f;
    private Vector3 originalPos;
    public float growAmount = 0.0001f;
    public float maxSwellFactor = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    public void Grow()
    {
        if (swellFactor < maxSwellFactor)
        {
            swellFactor += growAmount;
        }

    }

    public float Size()
    {
        return swellFactor / maxSwellFactor;
    }

    public void GetNewSwell(float newSwell)
    {
        float tempSwell = GameUtils.Map(newSwell, 0, 1.0f, swellMin, swellMax);
        swell = swellOffset + tempSwell;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.localScale;
        temp.x = swell * swellFactor;
        temp.y = swell * swellFactor;
        temp.z = swell * swellFactor;

        transform.localScale = temp;
    }


}
