using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseControl : MonoBehaviour
{
    public int spawnPosition;

    public Material originalMaterial_Body;
    public Material transparentMaterial_Body;

    public Material originalMaterial_Mascara;
    public Material transparentMaterial_Mascara;

    public float timerInvisible = 10f;

    public GameObject hideOrb;

    public GameObject Mesh_Body;
    public GameObject Mesh_Mascara;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hideOrb.activeSelf)
        {
            Mesh_Body.GetComponent<SkinnedMeshRenderer>().material = originalMaterial_Body;
            Mesh_Mascara.GetComponent<MeshRenderer>().material = originalMaterial_Mascara;
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
            Mesh_Body.GetComponent<SkinnedMeshRenderer>().material = transparentMaterial_Body;
            Mesh_Mascara.GetComponent<MeshRenderer>().material = transparentMaterial_Mascara;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("HideOrb"))
        {
            Mesh_Body.GetComponent<SkinnedMeshRenderer>().material = originalMaterial_Body;
            Mesh_Mascara.GetComponent<MeshRenderer>().material = originalMaterial_Mascara;
        }
    }
}
