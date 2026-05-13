using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AmbushVisualEffects : MonoBehaviour
{
    public Light[] lightsToFlicer;
    
    public AudioSource ambushAudio;

    public Volume globalVolume;

    private Vignette vignette;

    private void Start()
    {
        globalVolume.profile.TryGet(out vignette);

    }

    public void PlayAmbushVisualEffect()
    {
        StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        ambushAudio.Play();

        for (int i=0; i<2; i++)
        {
            SetLights(false);
            yield return new WaitForSeconds(0.1f);

            SetLights(true);
            yield return new WaitForSeconds(0.1f);
        }

        vignette.color.Override(Color.red);
        yield return new WaitForSeconds(3f);

        vignette.color.Override(Color.black);
    }

    private void SetLights(bool enabled)
    {
        foreach(Light light in lightsToFlicer)
        {
            light.enabled=enabled;
        }
    }
}
