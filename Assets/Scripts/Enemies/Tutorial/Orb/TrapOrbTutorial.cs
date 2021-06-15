using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOrbTutorial : MonoBehaviour
{
    public ParticleSystem auraParticlesDisableTrap;
    public GameObject trapTutorial;

    public void ActivateAuraParticles()
    {
        auraParticlesDisableTrap.Play();
        Invoke("DeactivateTrap", 2f);
    }

    public void DeactivateTrap()
    {
        trapTutorial.GetComponent<TrapTutorialController>().DeactivateTrap();
        auraParticlesDisableTrap.Stop();
    }

}
