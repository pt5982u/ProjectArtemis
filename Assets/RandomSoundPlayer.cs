using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public List<GameObject> soundObjects = new List<GameObject>();
    public float minInterval = 1f; // Minimum time between sounds
    public float maxInterval = 3f; // Maximum time between sounds

    private float timer;

    private void Start()
    {
        // Find all GameObjects with AudioSource components
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<AudioSource>() != null)
            {
                soundObjects.Add(obj);
            }
        }

        // Start the timer
        timer = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PlayRandomSound();

            // Reset the timer with a new random interval
            timer = Random.Range(minInterval, maxInterval);
        }
    }

    private void PlayRandomSound()
    {
        if (soundObjects.Count == 0)
        {
            Debug.LogWarning("No GameObjects with AudioSource components found.");
            return;
        }

        int randomIndex = Random.Range(0, soundObjects.Count);
        GameObject randomSoundObject = soundObjects[randomIndex];

        // Check if the AudioSource is playing before playing again
        if (!randomSoundObject.GetComponent<AudioSource>().isPlaying)
        {
            randomSoundObject.GetComponent<AudioSource>().Play();
        }
    }
}
