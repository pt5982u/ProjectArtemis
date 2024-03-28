using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererToggle : MonoBehaviour
{
    public float minToggleInterval = 1f;
    public float maxToggleInterval = 3f;

    private MeshRenderer meshRenderer;
    private float nextToggleTime;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogError("Mesh Renderer component not found on the object: " + gameObject.name);
            enabled = false; // Disable the script if Mesh Renderer is not found
        }

        // Set initial toggle time
        nextToggleTime = Time.time + Random.Range(minToggleInterval, maxToggleInterval);
    }

    void Update()
    {
        // Check if Mesh Renderer is null
        if (meshRenderer == null)
            return;

        // Check if it's time to toggle the Mesh Renderer
        if (Time.time >= nextToggleTime)
        {
            // Toggle the Mesh Renderer on/off
            meshRenderer.enabled = !meshRenderer.enabled;

            // Set next toggle time
            nextToggleTime = Time.time + Random.Range(minToggleInterval, maxToggleInterval);
        }
    }
}

