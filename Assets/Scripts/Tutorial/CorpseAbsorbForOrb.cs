using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseAbsorbForOrb : MonoBehaviour
{
    public Transform Target;

	public ParticleSystem system;

    public ParticleSystem subSystem;
    public ParticleSystem orbSystem;
    public ParticleSystem orbSubSystem;
    public ParticleSystem auraSystem;

    public GameObject disappearParticles;
    public float absorbDuration = 2f;
    float currentAbsorbTime = 0f;
	private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[100];

    void OnEnable()
    {
        if (system == null)
			system = GetComponent<ParticleSystem>();

		if (system == null){
			this.enabled = false;
		}
        //lightAuraSystem.Play();
        auraSystem.Play();
        system = orbSystem.GetComponent<ParticleSystem>();
        subSystem = orbSubSystem.GetComponent<ParticleSystem>();
        system.Play();
        subSystem.Play();
        //StartCoroutine(Wait(absorbDuration));
        //Target = null;
    }
	void Start() 
    {
        
	}

    // Update is called once per frame
    void Update()
    {
        ParticlesEmission(system);
        ParticlesEmission(subSystem);
    }

    /*public void AbsorbParticles(float particleDuration, GameObject target)
    {
        system.Play();
        absorbDuration = particleDuration;
        StartCoroutine(Wait(particleDuration));
        Target = target.transform;
        currentAbsorbTime = 0f;
    }*/

    /*public void AbsorbOrbParticles(GameObject target)
    {
        system.Play();
        Target = target.transform;
        currentAbsorbTime = 0f;
        absorbDuration = 1;
    }*/

    void ParticlesEmission(ParticleSystem partSystem)
    {
        partSystem.transform.LookAt(Target.transform.position, partSystem.gameObject.transform.up);
        int length = partSystem.GetParticles (particles);
        //ParticleSystem.ShapeModule shape = system.shape;
        Vector3 attractorPosition = Target.position;
        //shape.length = Vector3.Distance(system.transform.position, attractorPosition);
        //Debug.Log(attractorPosition + " " + Target.name);
        for (int i=0; i < length; i++) 
        {
            
            if(Vector3.Distance(partSystem.transform.TransformPoint(particles[i].position), attractorPosition) < 0.4f)
            {
                particles[i].position = Target.position;
                particles[i].velocity = new Vector3(0,0,0);
                //Debug.Log((transform.TransformPoint( particles[i].position) - Target.position).magnitude);
                particles[i].remainingLifetime =  0;
                //particles[i].position = Target.position;
            }
            //Debug.Log(system.transform.TransformPoint(particles[i].position) + " " + attractorPosition + " " + (system.transform.TransformPoint( particles[i].position) - Target.position).magnitude);
        }
        partSystem.SetParticles (particles, length);
    }
    IEnumerator Wait(float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        StopAbsortion();
        Instantiate(disappearParticles, system.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        
    }

    public void StopAbsortion()
    {
        system.Clear();
        system.Stop();
        currentAbsorbTime = 0f;
            
        //Target = null;
    }
}
