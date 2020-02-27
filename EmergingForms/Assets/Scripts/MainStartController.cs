using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainStartController : MonoBehaviour
{
    public CameraController cameraController;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp("space") )
        {
            anim.Play("FadeOut");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("FadeOut"))
        {
            cameraController.Begin();
        }

    }
}
