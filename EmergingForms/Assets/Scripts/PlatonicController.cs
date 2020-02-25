using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public float absorbRate = 2.0f;
    public float absorbThreshold = 0.75f;
    public Swell swell;
    private float absorbAmount = 0;
    private int didEat = 0;
    private float size = 0;


    void Start()
    {
        parentController = platonicParent.GetComponent<PlatonicParentController>();
        swell = GetComponent<Swell>();
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
            anim.Play("PlatonicGodFadeOut");
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Entity") && parentController.NumActivePlatonics > 0 && absorbAmount > absorbThreshold)
        {
            NumEntitiesAbsorbed++;
            other.GetComponent<EntityController>().Die();
            Destroy(other.gameObject);
            swell.Grow();
            DidEat = 1;
            Size = GameUtils.Map(swell.Size(), 0, swell.maxSwellFactor, 0, 1.0f);
        }
    }
}
