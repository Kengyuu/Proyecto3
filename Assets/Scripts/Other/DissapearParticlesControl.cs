using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearParticlesControl : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        Invoke("DestroyGO", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyGO()
    {
        Destroy(gameObject);
    }
}
