using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains : MonoBehaviour
{
    public GameObject chainsInv;
    public GameObject chainsTrace;

    public void SetInvToFalse()
    {
        chainsInv.gameObject.SetActive(false);
    }
    public void SetTraceToFalse()
    {
        chainsTrace.gameObject.SetActive(false);
    }
}
