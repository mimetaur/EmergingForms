using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialXFade : MonoBehaviour
{
    public Material source;
    public Material destination;

    private Renderer rend;
    private float fadeValue;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = source;
    }

    public void GetNewFadeValue(float newFadeValue)
    {
        fadeValue = newFadeValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeValue != 0)
        {
            rend.material.Lerp(source, destination, fadeValue);
        }

    }
}

// using UnityEngine;

// public class Example : MonoBehaviour
// {
//     // Blends between two materials

//     Material material1;
//     Material material2;
//     float duration = 2.0f;
//     Renderer rend;

//     void Start()
//     {
//         rend = GetComponent<Renderer>();

//         // At start, use the first material
//         rend.material = material1;
//     }

//     void Update()
//     {
//         // ping-pong between the materials over the duration
//         float lerp = Mathf.PingPong(Time.time, duration) / duration;
//         rend.material.Lerp(material1, material2, lerp);
//     }
// }
