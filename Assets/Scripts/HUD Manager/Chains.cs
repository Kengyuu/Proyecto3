using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains : MonoBehaviour
{
    public OrbSpawner spawner;
    

    public void SetToFalse()
    {
        Debug.Log("false");
        spawner.showingOrb = false;
    }

    public void SetToTrue()
    {
        Debug.Log("true");
        spawner.showingOrb = true;
    }

}
