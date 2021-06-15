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
    void Start()
    {
        GM = GameManager.Instance;
    }

    void OnEnable()
    {
        if (mainParticles == null)
			mainParticles = GetComponent<ParticleSystem>();

		if (mainParticles == null){
			this.enabled = false;
		}
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
                    }
                    break;
                case "CorpseOrb":
                    absorberStunned = finalPoint.GetComponent<FSM_ReturnToSafety_Corpse>().killed;
                    break;
            }
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
    }
}
