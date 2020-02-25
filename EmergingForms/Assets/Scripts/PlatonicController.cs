using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class PlatonicController : MonoBehaviour
{
    private const int ACTIVE_STATE = 1;
    private const int INACTIVE_STATE = 0;
    private bool isActive = false;
    private Animator anim;
    private PlatonicParentController parentController;
    private int numEntitiesAbsorbed = 0;

    public GameObject platonicParent;
    public bool IsActive { get => isActive; set => isActive = value; }
    public int NumEntitiesAbsorbed { get => numEntitiesAbsorbed; set => numEntitiesAbsorbed = value; }
    public int DidEat { get => didEat; set => didEat = value; }
    public float Size { get => size; set => size = value; }
    public int DidEatMoreThanThreshold { get => didEatMoreThanThreshold; set => didEatMoreThanThreshold = value; }

    public float spinRate = 10f;
    public float absorbThreshold = 0.75f;
    public Swell swell;
    private float absorbAmount = 0;
    private int didEat = 0;
    private float size = 0;
    private Renderer rend;

    public int eatNumThreshold = 50;
    private int didEatMoreThanThreshold = 0;

    void Start()
    {
        parentController = platonicParent.GetComponent<PlatonicParentController>();
        swell = GetComponent<Swell>();
        rend = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        anim.Play("PlatonicGodHidden");
    }

    public void ToggleActive(int newIsActive)
    {
        if (newIsActive == ACTIVE_STATE)
        {
            anim.Play("PlatonicGodFadeIn");
            IsActive = true;
        }
        else if (newIsActive == INACTIVE_STATE)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PlatonicGodHidden"))
            {
                anim.Play("PlatonicGodFadeOut");
            }

            IsActive = false;
        }
    }

    public void GetNewAbsorbtionAmount(float newAmount)
    {
        absorbAmount = newAmount;
    }

    void Update()
    {
        DidEat = 0;
        DidEatMoreThanThreshold = 0;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlatonicGodHidden"))
        {
            rend.shadowCastingMode = ShadowCastingMode.Off;
            rend.enabled = false;
        }
        else
        {
            rend.shadowCastingMode = ShadowCastingMode.On;
            rend.enabled = true;
        }

        Spin();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Entity") && parentController.NumActivePlatonics > 0 && absorbAmount > absorbThreshold)
        {
            other.GetComponent<EntityController>().Die();
            Destroy(other.gameObject);

            swell.Grow();
            Size = GameUtils.Map(swell.Size(), 0, swell.maxSwellFactor, 0, 1.0f);

            NumEntitiesAbsorbed++;
            Debug.Log(gameObject.name + "has eaten " + NumEntitiesAbsorbed + " entities");
            if (NumEntitiesAbsorbed > eatNumThreshold)
            {
                DidEatMoreThanThreshold = 1;
                NumEntitiesAbsorbed = 0;
            }

            DidEat = 1;
        }
    }

    private void Spin()
    {
        transform.Rotate(0, spinRate * Time.deltaTime, 0);
    }
}
