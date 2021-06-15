using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseOrbTutorial : MonoBehaviour
{
    public GameObject target;

    public void ArsorbCorpse()
    {
        target.GetComponent<CorpseAbsorbTutorial>().AbsorbOrbParticles(gameObject);
    }
}
