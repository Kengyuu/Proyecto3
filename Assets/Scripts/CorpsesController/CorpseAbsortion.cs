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
		
		if(systemActive && currentAbsorbTime <= absorbDuration)
        {
            switch(Target.tag)
            {
                case "Player":
                    absorberStunned = GM.GetPlayer().GetComponent<PlayerController>().m_PlayerStunned;
                    //Target.position = GM.GetPlayer().transform.position;
                    break;
                case "Enemy":
                    absorberStunned = Target.GetComponent<HFSM_StunEnemy>().isStunned;
                    if(Target.GetComponent<EnemyPriorities>().playerSeen || Target.GetComponent<EnemyPriorities>().playerDetected)
                    {
                        systemActive = false;
                        absorberStunned = true;
                        StopAbsortion();
                    }
                    break;
                case "CorpseOrb":
                    absorberStunned = Target.GetComponent<FSM_ReturnToSafety_Corpse>().killed;
                    break;
            }

            if(!absorberStunned)
            {
                if(Target != null)
                {
                    system.transform.LookAt(Target.transform.position, system.gameObject.transform.up);
                    int length = system.GetParticles (particles);
                    Vector3 attractorPosition = Target.position;
        
                    for (int i=0; i < length; i++) 
                    {
                        Vector3 distanceParticleTarget = (attractorPosition - system.transform.TransformPoint(particles[i].position)).normalized;
                        //particles[i].remainingLifetime = distanceParticleTarget.magnitude / particles[i].velocity.magnitude;
                        Vector3 particleSpeed = Vector3.Distance(system.transform.TransformPoint(particles[i].position), attractorPosition) * distanceParticleTarget / (particles[i].remainingLifetime);
                        particles[i].position += particleSpeed * Time.deltaTime;
                        if(Vector3.Distance(system.transform.TransformPoint(particles[i].position), attractorPosition) < 0.5f)
                        {
                            particles[i].position = Target.position;
                            //particles[i].velocity = new Vector3(0,0,0);
                            //Debug.Log((transform.TransformPoint( particles[i].position) - Target.position).magnitude);
                            particles[i].remainingLifetime =  0;
                            //particles[i].position = Target.position;
                        }
                        //Debug.Log(system.transform.TransformPoint(particles[i].position) + " " + attractorPosition + " " + (system.transform.TransformPoint( particles[i].position) - Target.position).magnitude);
                    }
                    system.SetParticles (particles, length);
                    currentAbsorbTime += Time.deltaTime;
                }
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
        if(Target != null && Target.CompareTag("Player"))
        {
            GameObject player = GM.GetPlayer();
            player.GetComponent<PlayerShoot>().ResetShoot();
        }
        Target = null;
    }
}