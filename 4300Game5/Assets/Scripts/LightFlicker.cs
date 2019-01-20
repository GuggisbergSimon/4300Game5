using UnityEngine;
using System.Collections;
     
public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float maxReduction;
    [SerializeField] private float maxIncrease;
    [SerializeField] private float rateDamping;
    [SerializeField] private float strength;
    [SerializeField] private bool stopFlickering;
     
    private Light lightSource;
    private float baseIntensity;
    private bool flickering;
     
    public void Reset()
    {
        maxReduction = 0.2f;
        maxIncrease = 0.2f;
        rateDamping = 0.1f;
        strength = 300;
    }
     
    public void Start()
    {
        lightSource = GetComponent<Light>();
        if (lightSource == null)
        {
            Debug.LogError("Flicker script must have a Light Component on the same GameObject.");
            return;
        }
        baseIntensity = lightSource.intensity;
        StartCoroutine(DoFlicker());
    }
     
    void Update()
    {
        if (!stopFlickering && !flickering)
        {
            StartCoroutine(DoFlicker());
        }
    }
     
    private IEnumerator DoFlicker()
    {
        flickering = true;
        while (!stopFlickering)
        {
            lightSource.intensity = Mathf.Lerp(lightSource.intensity, Random.Range(baseIntensity - maxReduction, baseIntensity + maxIncrease), strength * Time.deltaTime);
            yield return new WaitForSeconds(rateDamping);
        }
        flickering = false;
    }
}
