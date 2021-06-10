using UnityEngine;
using System.Collections;

public class CorpseAbsortion : MonoBehaviour {
	public Transform Target;

	public ParticleSystem system;
    public ParticleSystem subSystem;
    public ParticleSystem playerSystem;
    public ParticleSystem playerSubSystem;
    public ParticleSystem enemySystem;
    public ParticleSystem enemySubSystem;
    public ParticleSystem orbSystem; 
    public ParticleSystem orbSubSystem; 
    public ParticleSystem auraSystem;
    //public ParticleSystem lightAuraSystem;
    public bool systemActive = false;

    float absorbDuration;
    float currentAbsorbTime = 0f;
    bool absorberStunned = false;

    private GameManager GM;
	private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[100];
    private static ParticleSystem.Particle[] subParticles = new ParticleSystem.Particle[100];

	int count;
    void OnEnable()
    {
        /*if (system == null)
			system = GetComponent<ParticleSystem>();
        if(subSystem == null)
            subSystem = GetComponent<ParticleSystem>();

		if (system == null)
            this.enabled = false;   
        if (subSystem == null)
            this.enabled = false;  */
        //lightAuraSystem.Play();
        auraSystem.Play();
        Target = null;
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
                case "AbsorbObjective":
                    absorberStunned = GM.GetPlayer().GetComponent<PlayerController>().m_PlayerStunned;
                    if(absorberStunned)
                    {
                        systemActive = false;
                        StopAbsortion();
                    }
                    //Target.position = GM.GetPlayer().transform.position;
                    break;
                case "AbsorbObjectiveEnemy":
                    absorberStunned = GM.GetEnemy().GetComponent<HFSM_StunEnemy>().isStunned;
                    if (GM.GetEnemy().GetComponent<EnemyPriorities>().playerSeen || GM.GetEnemy().GetComponent<EnemyPriorities>().playerDetected)
                    {
                        systemActive = false;
                        absorberStunned = true;
                        StopAbsortion();
                    }
                    break;
                case "CorpseOrb":
                    absorberStunned = Target.GetComponent<FSM_ReturnToSafety_Corpse>().killed;
                    if (absorberStunned)
                    {
                        systemActive = false;
                        StopAbsortion();
                    }
                    break;
            }

            if(!absorberStunned)
            {
                if(Target != null)
                {
                    ParticlesEmission(system);
                    if(!Target.CompareTag("AbsorbObjective"))
                    {
                        ParticlesEmission(subSystem);
                    }
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
        switch(target.tag)
        {
            case "AbsorbObjective":
                system = playerSystem.GetComponent<ParticleSystem>();
                break;
            case "AbsorbObjectiveEnemy":
                system = enemySystem.GetComponent<ParticleSystem>();
                subSystem = enemySubSystem.GetComponent<ParticleSystem>();
                break;
            case "CorpseOrb":
                system = orbSystem.GetComponent<ParticleSystem>();
                subSystem = orbSubSystem.GetComponent<ParticleSystem>();
                break;
        }
        system.Play();
        subSystem.Play();
        absorbDuration = particleDuration;
        StartCoroutine(Wait(particleDuration));
        Target = target.transform;
        systemActive = true;
        currentAbsorbTime = 0f;
    }

    /*void ParticlesPlayer()
    {
        system.transform.LookAt(Target.transform.position, system.gameObject.transform.up);
        int length = system.GetParticles (particles);
        //ParticleSystem.ShapeModule shape = system.shape;
        Vector3 attractorPosition = Target.position;
        //shape.length = Vector3.Distance(system.transform.position, attractorPosition);
        //Debug.Log(attractorPosition + " " + Target.name);
        for (int i=0; i < length; i++) 
        {
            /*Vector3 distanceParticleTarget = (attractorPosition - system.transform.TransformPoint(particles[i].position)).normalized;
            Debug.Log(distanceParticleTarget);
            //particles[i].remainingLifetime = distanceParticleTarget.magnitude / particles[i].velocity.magnitude;
            Vector3 particleSpeed = Vector3.Distance(system.transform.TransformPoint(particles[i].position), attractorPosition) * distanceParticleTarget / (particles[i].remainingLifetime);
            particles[i].position += particleSpeed * Time.deltaTime;
            
            if(Vector3.Distance(system.transform.TransformPoint(particles[i].position), attractorPosition) < 0.5f)
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
    }*/

    void ParticlesEmission(ParticleSystem partSystem)
    {
        partSystem.transform.LookAt(Target.transform.position, system.gameObject.transform.up);
        int length = partSystem.GetParticles (particles);
        //ParticleSystem.ShapeModule shape = system.shape;
        Vector3 attractorPosition = Target.position;
        //shape.length = Vector3.Distance(system.transform.position, attractorPosition);
        //Debug.Log(attractorPosition + " " + Target.name);
        for (int i=0; i < length; i++) 
        {
            /*Vector3 distanceParticleTarget = (attractorPosition - system.transform.TransformPoint(particles[i].position)).normalized;
            Debug.Log(distanceParticleTarget);
            //particles[i].remainingLifetime = distanceParticleTarget.magnitude / particles[i].velocity.magnitude;
            Vector3 particleSpeed = Vector3.Distance(system.transform.TransformPoint(particles[i].position), attractorPosition) * distanceParticleTarget / (particles[i].remainingLifetime);
            particles[i].position += particleSpeed * Time.deltaTime;*/
            
            if(Vector3.Distance(partSystem.transform.TransformPoint(particles[i].position), attractorPosition) < 0.5f)
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
        currentAbsorbTime += Time.deltaTime;
    }

    /*void ParticlesEnemies()
    {
        system.transform.LookAt(Target.transform.position, system.gameObject.transform.up);
        int length = system.GetParticles (particles);
        //ParticleSystem.ShapeModule shape = system.shape;
        Vector3 attractorPosition = Target.position;
        //shape.length = Vector3.Distance(system.transform.position, attractorPosition);
        //Debug.Log(attractorPosition + " " + Target.name);
        for (int i=0; i < length; i++) 
        {
            /*Vector3 distanceParticleTarget = (attractorPosition - system.transform.TransformPoint(particles[i].position)).normalized;
            Debug.Log(distanceParticleTarget);
            //particles[i].remainingLifetime = distanceParticleTarget.magnitude / particles[i].velocity.magnitude;
            Vector3 particleSpeed = Vector3.Distance(system.transform.TransformPoint(particles[i].position), attractorPosition) * distanceParticleTarget / (particles[i].remainingLifetime);
            particles[i].position += particleSpeed * Time.deltaTime;
            
            if(Vector3.Distance(system.transform.TransformPoint(particles[i].position), attractorPosition) < 0.5f)
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


    }*/

    IEnumerator Wait(float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        if(systemActive)
        {
            /*if (GM.GetPlayer().GetComponent<PlayerShoot>().m_PlayerAnimations.m_Animator.GetBool("Absorb"))
            {
                
            }*/
            StopAbsortion();

            gameObject.SetActive(false);
            if (GM.m_GameObjectSpawner != null)
                GM.m_GameObjectSpawner.ClearBodys(gameObject.GetComponent<CorpseControl>().spawnPosition);


        }
        else
        {
            system.Clear();
            subSystem.Clear();
            system.Stop();
            subSystem.Stop();
            currentAbsorbTime = 0f;
        }
        
    }
    public void StopAbsortion()
    {
        systemActive = false;
        system.Clear();
        subSystem.Clear();
        system.Stop();
        subSystem.Stop();
        currentAbsorbTime = 0f;
        if(Target != null)
            Target = null;
        /*if(Target != null && Target.CompareTag("AbsorbObjective"))
        {
            Target.gameObject.SetActive(false);
            //GameObject player = GM.GetPlayer();
            //player.GetComponent<PlayerShoot>().ResetShoot();
        }*/
        //Target = null;
    }
}