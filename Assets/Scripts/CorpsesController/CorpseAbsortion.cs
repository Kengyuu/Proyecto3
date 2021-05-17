using UnityEngine;
using System.Collections;

public class CorpseAbsortion : MonoBehaviour {
	public Transform Target;

	private ParticleSystem system;

    public ParticleSystem auraSystem;
    bool systemActive = false;

    float absorbDuration;
    float currentAbsorbTime = 0f;
    bool absorberStunned = false;

    private GameManager GM;
	private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];

	int count;

	void OnEnable() {
        GM = GameManager.Instance;
        //Target = GM.GetPlayer().transform;
		if (system == null)
			system = GetComponent<ParticleSystem>();

		if (system == null){
			this.enabled = false;
		}
        if(auraSystem != null)
        /*else{
			system.Play();
		}*/
        system.Stop();
        auraSystem.Play();
	}
	void Update(){
		
		if(systemActive && currentAbsorbTime <= absorbDuration)
        {
            switch(Target.tag)
            {
                case "Player":
                    absorberStunned = Target.GetComponent<PlayerController>().m_PlayerStunned;
                    break;
                case "Enemy":
                    absorberStunned = Target.GetComponent<HFSM_StunEnemy>().isStunned;
                    if(Target.GetComponent<EnemyPriorities>().playerSeen || Target.GetComponent<EnemyPriorities>().playerDetected)
                    {
                        systemActive = false;
                    }
                    break;
                case "CorpseOrb":
                    absorberStunned = Target.GetComponent<FSM_ReturnToSafety_Corpse>().killed;
                    break;
            }

            if(!absorberStunned)
            {
                count = system.GetParticles(particles);
                for (int i = 0; i < count; i++)
                {
                    
                    ParticleSystem.Particle particle = particles[i];
                    Vector3 v1 = system.transform.TransformPoint(particle.position);
                    Vector3 v2 = Target.transform.position;
                    Vector3 tarPosi = (v2 - v1) *  (particle.remainingLifetime / particle.startLifetime);
                    particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
                    particles[i] = particle;
                }
                system.SetParticles(particles, count);
                currentAbsorbTime += Time.deltaTime;

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
            systemActive = false;
            system.Stop();
            gameObject.SetActive(false);
            GM.m_GameObjectSpawner.ClearBodys(GetComponent<CorpseControl>().spawnPosition);
        }
        
    }
    public void StopAbsortion()
    {
        systemActive = false;
        system.Stop();
        currentAbsorbTime = 0f;
        Target = null;
    }
}