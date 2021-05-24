using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseControl : MonoBehaviour
{
    public int spawnPosition;

    public Material originalMaterial;
    public Material transparentMaterial;

    public float timerInvisible = 10f;

    public GameObject hideOrb;

    public GameObject mesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hideOrb.activeSelf)
        {
            mesh.GetComponent<MeshRenderer>().material = originalMaterial;
        }
        /*if(changeVisibility)
        {
            GetComponent<MeshRenderer>().material = transparentMaterial;
            timerInvisible -= Time.deltaTime;
            if(timerInvisible <= 0)
            {
                timerInvisible = 10f;
                GetComponent<MeshRenderer>().material = originalMaterial;
                changeVisibility = false;
            }
        }
        else
        {
            GetComponent<MeshRenderer>().material = originalMaterial;
        }*/
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("HideOrb"))
        {
            mesh.GetComponent<MeshRenderer>().material = transparentMaterial;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("HideOrb"))
        {
            mesh.GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }
}
