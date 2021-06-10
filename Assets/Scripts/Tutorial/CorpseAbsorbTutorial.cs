using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseAbsorbTutorial : MonoBehaviour
{
    public Transform Target;

	public ParticleSystem system;

    public ParticleSystem playerSystem;
    public ParticleSystem auraSystem;

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
        if (system == null)
			system = GetComponent<ParticleSystem>();

		if (system == null){
			this.enabled = false;
		}
        //lightAuraSystem.Play();
        auraSystem.Play();
        Target = null;
    }
	void Start() {
        GM = GameManager.Instance;
        
	}

    // Update is called once per frame
    void Update()
    {
        if(systemActive && currentAbsorbTime <= absorbDuration)
        {

            if(Target != null)
            {
                
                system.transform.LookAt(Target.transform.position, system.gameObject.transform.up);
                int length = system.GetParticles (particles);
                //ParticleSystem.ShapeModule shape = system.shape;
                Vector3 attractorPosition = Target.position;
                //shape.length = Vector3.Distance(system.transform.position, attractorPosition);
                //Debug.Log(attractorPosition + " " + Target.name);
                for (int i=0; i < length; i++) 
                {
                    
                    if(Vector3.Distance(system.transform.TransformPoint(particles[i].position), attractorPosition) < 0.4f)
                    {
                        particles[i].position = Target.position;
                        particles[i].velocity = new Vector3(0,0,0);
                        //Debug.Log((transform.TransformPoint( particles[i].position) - Target.position).magnitude);
                        particles[i].remainingLifetime =  0;
                        //particles[i].position = Target.position;
                    }
                    //Debug.Log(system.transform.TransformPoint(particles[i].position) + " " + attractorPosition + " " + (system.transform.TransformPoint( particles[i].position) - Target.position).magnitude);
                }
                system.SetParticles (particles, length);
                currentAbsorbTime += Time.deltaTime;
                if(Target.CompareTag("Untagged"))
                    currentAbsorbTime = 0;
            }
        }
    }

    public void AbsorbParticles(float particleDuration, GameObject target)
    {
        system.Play();
        absorbDuration = particleDuration;
        StartCoroutine(Wait(particleDuration));
        Target = target.transform;
        systemActive = true;
        currentAbsorbTime = 0f;
    }

    public void AbsorbOrbParticles(GameObject target)
    {
        system.Play();
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
            gameObject.SetActive(false);
            
        }
        
    }

    public void StopAbsortion()
    {
        systemActive = false;
        system.Clear();
        system.Stop();
        currentAbsorbTime = 0f;
        if(Target != null && Target.CompareTag("AbsorbObjective"))
        {
            GameObject player = GM.GetPlayer();
            //player.GetComponent<PlayerShoot>().ResetShoot();
        }
        //wpControllerTutorial.RestartWeakPoints();
        if(subRoom != null)
        {
            subRoom.SetActive(true);
            roomDecal.SetActive(true);
        }
        if(wpControllerTutorial.currentPhase < 3)
        {
            wpControllerTutorial.RestartWeakPoints();
        }
        else
        {
            wpControllerTutorial.TutorialControl();
        }
        
            
        //Target = null;
    }
}
