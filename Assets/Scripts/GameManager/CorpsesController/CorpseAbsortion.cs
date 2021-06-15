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
    public GameObject disappearParticles;
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
        auraSystem.Play();
        Target = null;
    }
	void Start() {
        GM = GameManager.Instance;
	}
	void Update(){
		
		if(systemActive && currentAbsorbTime <= absorbDuration)
        {
            if(Target != null)
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
                    case "AbsorbObjectiveWatcher":
                        absorberStunned = Target.parent.GetComponent<FSM_ReturnToSafety_Corpse>().killed;
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
                        ParticlesEmission(subSystem);
                        currentAbsorbTime += Time.deltaTime;
                    }
                }
                else
                {
                    StopAbsortion();
                }
            }
            else
            {
                systemActive = false;
            }
            
        }
	}

    public void AbsorbParticles(float particleDuration, GameObject target)
    {
        switch(target.tag)
        {
            case "AbsorbObjective":
                system = playerSystem.GetComponent<ParticleSystem>();
                subSystem = playerSubSystem.GetComponent<ParticleSystem>();
                break;
            case "AbsorbObjectiveEnemy":
                system = enemySystem.GetComponent<ParticleSystem>();
                subSystem = enemySubSystem.GetComponent<ParticleSystem>();
                break;
            case "AbsorbObjectiveWatcher":
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
    void ParticlesEmission(ParticleSystem partSystem)
    {
        partSystem.transform.LookAt(Target.transform.position, partSystem.gameObject.transform.up);
        int length = partSystem.GetParticles (particles);
        Vector3 attractorPosition = Target.position;
        for (int i=0; i < length; i++) 
        {
            if(Vector3.Distance(partSystem.transform.TransformPoint(particles[i].position), attractorPosition) < 0.5f)
            {
                particles[i].position = Target.position;
                particles[i].velocity = new Vector3(0,0,0);
                particles[i].remainingLifetime =  0;
            }
        }
        partSystem.SetParticles (particles, length);
       
    }

    IEnumerator Wait(float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        if(systemActive)
        {
            StopAbsortion();

            gameObject.SetActive(false);
            if (GM.m_GameObjectSpawner != null)
                GM.m_GameObjectSpawner.ClearBodys(gameObject.GetComponent<CorpseControl>().spawnPosition);
            Instantiate(disappearParticles, system.transform.position, Quaternion.identity);

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
    }
}