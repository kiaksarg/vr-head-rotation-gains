using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;
public class GainTechniques : MonoBehaviour
{

    public enum GainTechniquesEnum
    {
        constant,
        dynamic,
        velocityGuided,
    }


    public GainTechniquesEnum Technique;
    public float ConstantGain = 2;
    public float Velocity = 20;
    public float minGain = 1;
    public float maxGain = 3;
    public float halfRotation = 90;
    public float CurrentGain = 0;
    public float VirtualAngle = 0;


    public float LastRotationAngle { get; set; } = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.eulerAngles.y != LastRotationAngle)
        {
            GameObject WordObjectScriptObject = GameObject.Find("VirtualRotationObjectCube");

            float gain = 0;
            switch (Technique)
            {
                case GainTechniquesEnum.constant:
                    gain = ConstantGain;
                    break;
                case GainTechniquesEnum.dynamic:
                    gain = RotationTechniques.dynamicNonLinearGain(WordObjectScriptObject.transform.eulerAngles.y, minGain, maxGain, halfRotation);
                    break;
                case GainTechniquesEnum.velocityGuided:
                    gain = RotationTechniques.velocityGuidedGain(Velocity, minGain, maxGain, halfRotation);
                    break;


            }


            var newEu = WordObjectScriptObject.transform.eulerAngles;

            if (Technique == GainTechniquesEnum.constant)
                newEu.y = gameObject.transform.eulerAngles.y * gain;
            else
            {
                //if forward rotation
                if (gameObject.transform.eulerAngles.y > LastRotationAngle)
                {
                    Debug.Log("forward");

                    newEu.y = (gameObject.transform.eulerAngles.y - LastRotationAngle) * gain + WordObjectScriptObject.transform.eulerAngles.y;
                }
                else
                {
                    newEu.y = (gameObject.transform.eulerAngles.y - LastRotationAngle) * CurrentGain + WordObjectScriptObject.transform.eulerAngles.y;
                    Debug.Log("rotate back");

                }
            }

            WordObjectScriptObject.transform.eulerAngles = newEu;

            CurrentGain = gain;
            VirtualAngle = WordObjectScriptObject.transform.eulerAngles.y;
            Debug.Log(newEu.y);
        }




        LastRotationAngle = gameObject.transform.eulerAngles.y;

        // WordObjectScriptObject.transform.rotation =
        // new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y * gain,
        //  gameObject.transform.rotation.z, gameObject.transform.rotation.w);
    }

    void FixedUpdate()
    {
        var txtVRotationComponent = GameObject.Find("VRotation").gameObject.GetComponent<TextMeshPro>();
        var txtGainComponent = GameObject.Find("Gain").gameObject.GetComponent<TextMeshPro>();
        txtVRotationComponent.text = "V-Angle: " + VirtualAngle.ToString("0.00") + "°";
        txtGainComponent.text = "Gain: " + CurrentGain;
    }
}
