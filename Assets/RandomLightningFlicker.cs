using UnityEngine;
using System.Collections;
public class RandomLightningFlicker : MonoBehaviour
{
    public Light lightningLight;
    public float minLightningDelay = 3f;
    public float maxLightningDelay = 10f;
    public float flickerDuration = 0.1f;
    public float lightningDuration = 0.2f;
    public float lightningIntensity = 10f;
    public float lightFadeDuration = 0.5f;
    private float lightningTimer;
    private bool isLightningActive;

    void Start()
    {
        lightningTimer = Random.Range(minLightningDelay, maxLightningDelay);
    }

    void Update()
    {
        lightningTimer -= Time.deltaTime;
        if (lightningTimer <= 0f && !isLightningActive)
        {
            AudioManager.instance.Play("Thunder");
            StartCoroutine(TriggerLightning());
        }
    }

    IEnumerator TriggerLightning()
    {
        isLightningActive = true;
        StartCoroutine(FlickerLightIntensity());
        yield return new WaitForSeconds(flickerDuration);
        StartCoroutine(FlickerLightIntensity());
        lightningLight.intensity = lightningIntensity;
        yield return new WaitForSeconds(lightningDuration);
        float elapsedTime = 0f;
        while (elapsedTime < lightFadeDuration)
        {
            float fadeAmount = Mathf.Lerp(lightningIntensity, 0f, elapsedTime / lightFadeDuration);
            lightningLight.intensity = fadeAmount;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lightningLight.intensity = 0f;
        lightningTimer = Random.Range(minLightningDelay, maxLightningDelay);
        isLightningActive = false;
    }

    IEnumerator FlickerLightIntensity()
    {
        lightningLight.intensity = 0;
        yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
        lightningLight.intensity = lightningIntensity;
    }
}