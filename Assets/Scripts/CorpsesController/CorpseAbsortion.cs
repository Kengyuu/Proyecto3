using UnityEngine;
using System.Collections;

public class CorpseAbsortion : MonoBehaviour {
	public Transform Target;

	public ParticleSystem system;

    public ParticleSystem auraSystem;
    //public ParticleSystem lightAuraSystem;
    bool systemActive = false;

    float absorbDuration;
    float currentAbsorbTime = 0f;
    bool absorberStunned = false;

    private GameManager GM;
	private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[100];

	int count;
    void OnEnable()
    {
        if (system == null)
			system = GetComponent<ParticleSystem>();

		if (system == null){
			this.enabled = false;
		}
        system.Stop();
        //lightAuraSystem.Play();
        auraSystem.Play();
    }
	void Start() {
        GM = GameManager.Instance;
        //Target = GM.GetPlayer().transform;
		
        /*else{
			system.Play();
		}*/
        
	}
	void Update(){
		
		if(systemActive&& currentAbsorbTime <= absorbDuration)
        {
            switch(Target.tag)
            {
                case "Player":
                    absorberStunned = Target.GetComponent<PlayerController>().m_PlayerStunned;
                    //Target.position = GM.GetPlayer().transform.position;
                    break;
                case "Enemy":
                    absorberStunned = Target.GetComponent<HFSM_StunEnemy>().isStunned;
                    if(Target.GetComponent<EnemyPriorities>().playerSeen || Target.GetComponent<EnemyPriorities>().playerDetected)
                    {
                        systemActive = false;
                        StopAbsortion();
                    }
                    break;
                case "CorpseOrb":
                    absorberStunned = Target.GetComponent<FSM_ReturnToSafety_Corpse>().killed;
                    break;
            }

            if(!absorberStunned)
            {
                /*if (system.isPlaying) 
                {
                    system.transform.LookAt(Target.transform.position, system.gameObject.transform.up);
                    int length = system.GetParticles (particles);
                    Vector3 attractorPosition = Target.position;
        
                    for (int i=0; i < length; i++) 
                    {
                        particles[i].position = particles[i].position + (attractorPosition - particles[i].position) / (particles[i].remainingLifetime) * Time.deltaTime;
                        /*if((particles[i].position - Target.position).magnitude < 0.2f)
                        {
                            particles[i].remainingLifetime =  0;
                        }
                    }
                    system.SetParticles (particles, length);
                }*/
                ParticleSystem.ShapeModule cone = system.shape;
                float distPartToTarget = (Target.position - system.transform.position).magnitude;
                //cone.length = distPartToTarget;
                system.transform.LookAt(Target.transform.position, system.gameObject.transform.up);
                
                //system.gameObject.transform.forward = (system.gameObject.transform.forward - Target.transform.forward).normalized;
                
                count = system.GetParticles(particles);
                for (int i = 0; i < count; i++)
                {
                    
                    ParticleSystem.Particle particle = particles[i];
                    Vector3 v1 = system.transform.TransformPoint(particle.position);
                    Vector3 v2 = Target.transform.position;
                    Vector3 tarPosi = (v1 - v2) *  (particle.remainingLifetime / particle.startLifetime);
                    particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
                    particles[i] = particle;
                    if(Vector3.Distance(transform.TransformPoint( particles[i].position), Target.position) < 100f)
                    {
                        Debug.Log("He entrado");
                        particles[i].remainingLifetime = 0;
                    }

                    Debug.Log(transform.TransformPoint( particles[i].position));
                    
                }
                //system.SetParticles(particles, count);
                currentAbsorbTime += Time.deltaTime;
                //currentAbsorbTime += Time.deltaTime;
                
            }
            else
            {
                StopAbsortion();
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
        //system.gameObject.transform.Rotate(gameObject.transform.forward, Vector3.Angle(gameObject.transform.forward, Target.transform.forward));
        //system.gameObject.transform.forward = system.gameObject.transform.forward - Target.transform.forward;
        /*count = system.GetParticles(particles);

		for (int i = 0; i < count; i++)
		{
            
			ParticleSystem.Particle particle = particles[i];

			Vector3 v1 = system.transform.TransformPoint(particle.position);
			Vector3 v2 = Target.transform.position;
			Vector3 tarPosi = (v2 - v1) *  (particle.remainingLifetime / particle.startLifetime);
			particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
			particles[i] = particle;
		}

		system.SetParticles(particles, count);*/
    }

    IEnumerator Wait(float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        if(systemActive)
        {
            StopAbsortion();
            gameObject.SetActive(false);
            GM.m_GameObjectSpawner.ClearBodys(gameObject.GetComponent<CorpseControl>().spawnPosition);
            
        }
        
    }
    public void StopAbsortion()
    {
        systemActive = false;
        system.Clear();
        system.Stop();
        currentAbsorbTime = 0f;
        Target = null;
    }
}