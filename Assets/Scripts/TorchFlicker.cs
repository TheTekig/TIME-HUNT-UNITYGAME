using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchFlicker : MonoBehaviour
{
    public Light2D light2D;

    public float minIntensity = 1.3f;
    public float maxIntensity = 1.8f;
    public float flickerSize;

    void Update()
    {
        //light2D.intensity = Random.Range(minIntensity, maxIntensity);
        light2D.pointLightOuterRadius = flickerSize + Random.Range(-0.1f, 0.1f);
    }
}