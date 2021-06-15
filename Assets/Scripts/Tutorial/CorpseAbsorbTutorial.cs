using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseAbsorbTutorial : MonoBehaviour
{
    public Transform Target;

	public ParticleSystem system;

    public ParticleSystem subSystem;
    public ParticleSystem playerSystem;

    public ParticleSystem playerSubSystem;
    public ParticleSystem auraSystem;
    public GameObject disappearParticles;

    public GameObject subRoom;
    public GameObject roomDecal;

    public bool systemActive = false;

    float absorbDuration;
    float currentAbsorbTime = 0f;
    private GameManager GM;
	private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[100];

    public WPControllerTutorial wpControllerTutorial;

    void OnEnable()
    {
        auraSystem.Play();
        Target = null;
    }
	void Start() {
        GM = GameManager.Instance; 
	}

    void Update()
    {
        if(systemActive && currentAbsorbTime <= absorbDuration)
        {

            if(Target != null)
            {
                ParticlesEmission(system);
                ParticlesEmission(subSystem);
                currentAbsorbTime += Time.deltaTime;
            }
        }
    }

    public void AbsorbParticles(float particleDuration, GameObject target)
    {
        system = playerSystem.GetComponent<ParticleSystem>();
        subSystem = playerSubSystem.GetComponent<ParticleSystem>();
        system.Play();
        subSystem.Play();
        absorbDuration = particleDuration;
        StartCoroutine(Wait(particleDuration));
        Target = target.transform;
        systemActive = true;
        currentAbsorbTime = 0f;
    }

    void ParticlesEmission(ParticleSystem partSystem)
    {
        partSystem.transform.LookAt(Target.transform.position, partSystem.gameObject.transform.up);
        int length = partSystem.GetParticles (particles);
        Vector3 attractorPosition = Target.position;
        for (int i=0; i < length; i++) 
        {
            
            if(Vector3.Distance(partSystem.transform.TransformPoint(particles[i].position), attractorPosition) < 0.4f)
            {
                particles[i].position = Target.position;
                particles[i].velocity = new Vector3(0,0,0);
                particles[i].remainingLifetime =  0;
            }
            
        }
        system.SetParticles(particles, length);
       
        if(Target.CompareTag("Untagged"))
            currentAbsorbTime = 0;
    }

    public void AbsorbOrbParticles(GameObject target)
    {
        system.Play();
        subSystem.Play();
        Target = target.transform;
        systemActive = true;
        currentAbsorbTime = 0f;
        absorbDuration = 1;
    }

    IEnumerator Wait(float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        if(systemActive)
        {
            StopAbsortion();
            Instantiate(disappearParticles, system.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            
        }
        
    }

    public void StopAbsortion()
    {
        systemActive = false;
        system.Clear();
        system.Stop();
        subSystem.Clear();
        subSystem.Stop();
        currentAbsorbTime = 0f;
        if(Target != null && Target.CompareTag("AbsorbObjective"))
        {
            GameObject player = GM.GetPlayer();
        }
        if(subRoom != null)
        {
            subRoom.SetActive(true);
            roomDecal.SetActive(true);
        }
      
    }
}
