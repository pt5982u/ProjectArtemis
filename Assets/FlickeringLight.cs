using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light flickeringLight;
    public AudioSource audioSource;
    public float minFlickerDuration = 0.1f;
    public float maxFlickerDuration = 0.5f;
    public float minFlickerIntensity = 0.5f;
    public float maxFlickerIntensity = 1f;
    public float soundFlickerChance = 0.3f;

    private float flickerTimer;
    private bool isFlickering = false;

    void Start()
    {
        flickerTimer = Random.Range(minFlickerDuration, maxFlickerDuration);
        flickeringLight.intensity = Random.Range(minFlickerIntensity, maxFlickerIntensity);
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (isFlickering)
            {
                flickeringLight.enabled = false;
                isFlickering = false;
            }
            return;
        }

        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= 0f)
        {
            if (Random.value < soundFlickerChance)
            {
                StartCoroutine(FlickerLight());
            }

            flickerTimer = Random.Range(minFlickerDuration, maxFlickerDuration);
        }
    }

    IEnumerator FlickerLight()
    {
        isFlickering = true;
        flickeringLight.enabled = true;

        // Randomly flicker the light
        flickeringLight.intensity = Random.Range(minFlickerIntensity, maxFlickerIntensity);
        yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
        flickeringLight.enabled = false;
        isFlickering = false;
    }
}
