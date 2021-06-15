using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearParticlesControl : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyGO", 2f);
    }
    void DestroyGO()
    {
        Destroy(gameObject);
    }
}
