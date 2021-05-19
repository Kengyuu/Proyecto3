using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEffect : MonoBehaviour
{
    public List<GameObject> objectivePoints;
    public GameObject startPoint;

    public GameObject finalPoint;
    ParticleSystem mainParticles;

    bool particlesActive = false;
    float absorbDuration;
    float currentAbsorbTime = 0f;
    bool absorberStunned = false;
    private GameManager GM;
    int count;
    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[200];
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
    }

    // Update is called once per frame

    void OnEnable()
    {
        if (mainParticles == null)
			mainParticles = GetComponent<ParticleSystem>();

		if (mainParticles == null){
			this.enabled = false;
		}
        //mainParticles.Play();
    }
    void Update()
    {
        if(particlesActive && currentAbsorbTime <= absorbDuration)
        {
            switch(finalPoint.tag)
            {
                case "Player":
                    absorberStunned = finalPoint.GetComponent<PlayerController>().m_PlayerStunned;
                    break;
                case "Enemy":
                    absorberStunned = finalPoint.GetComponent<HFSM_StunEnemy>().isStunned;
                    if(finalPoint.GetComponent<EnemyPriorities>().playerSeen || finalPoint.GetComponent<EnemyPriorities>().playerDetected)
                    {
                        particlesActive = false;
                        //StopAbsortion();
                    }
                    break;
                case "CorpseOrb":
                    absorberStunned = finalPoint.GetComponent<FSM_ReturnToSafety_Corpse>().killed;
                    break;
            }
        }

        if(!absorberStunned)
        {
            /*count = system.GetParticles(particles);
                for (int i = 0; i < count; i++)
                {
                    
                    /*ParticleSystem.Particle particle = particles[i];
                    Vector3 v1 = system.transform.TransformPoint(particle.position);
                    Vector3 v2 = Target.transform.position;
                    Vector3 tarPosi = (v1 - v2) *  (particle.remainingLifetime / particle.startLifetime);
                    particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
                    particles[i] = particle;*/
                    /*if((particles[i].position - Target.position).magnitude < 0.1f)
                    {
                        particles[i].remainingLifetime = 0;
                    }
                    
                }
                system.SetParticles(particles, count);
                currentAbsorbTime += Time.deltaTime;*/
        }
    }

    public void CreateParticles(float particleDuration, GameObject start, GameObject end)
    {
        mainParticles.Play();
        absorbDuration = particleDuration;
        StartCoroutine(Wait(absorbDuration));
        finalPoint = end;
    }

    IEnumerator Wait(float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        /*if(systemActive)
        {
            StopAbsortion();
            gameObject.SetActive(false);
            GM.m_GameObjectSpawner.ClearBodys(gameObject.GetComponent<CorpseControl>().spawnPosition);
            
        }*/
        
    }
}
