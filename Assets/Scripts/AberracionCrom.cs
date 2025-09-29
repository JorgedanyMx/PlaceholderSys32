using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class AberracionCrom : MonoBehaviour
{
    public Volume volume;
    private ChromaticAberration chromatic;
    public float intensity = 1;
    private LensDistortion lensDistortion;
    public float timeToDisable = 1;

    [Header("Oscilación")]
    [SerializeField] private float minIntensity = -0.5f;
    [SerializeField] private float maxIntensity = 0.5f;
    [SerializeField] private float speed = 2f;

    private void Start()
    {
        if (volume == null || !volume.profile.TryGet(out lensDistortion))
        {
            Debug.LogError("Volume o Lens Distortion no asignado correctamente.");
            enabled = false;
            return;
        }

        lensDistortion.active = true;
    }
    private void OnEnable()
    {
        StartCoroutine(DisableAfterSeconds());
    }
    private void Update()
    {
        float t = Mathf.Sin(Time.time * speed) * 0.5f + 0.5f; // Oscila entre 0 y 1
        lensDistortion.intensity.value = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
    private IEnumerator DisableAfterSeconds()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }

}
