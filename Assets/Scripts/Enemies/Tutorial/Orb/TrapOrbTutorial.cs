using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOrbTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem auraParticlesDisableTrap;
    public GameObject trapTutorial;
    void Start()
    {
        ActivateAuraParticles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
