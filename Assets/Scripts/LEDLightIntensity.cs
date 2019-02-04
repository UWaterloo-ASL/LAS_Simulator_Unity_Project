using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDLightIntensity : MonoBehaviour
{
    public float ledIntensity;

    public void SetLedIntensity(float intensity)
    {
        ledIntensity = intensity;
    }

    // Update is called once per frame
    void Update()
    {
        Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'
        Color finalColor = baseColor * ledIntensity;

        GetComponent<Renderer>().material.SetColor("_EmissionColor", finalColor);
    }
}
