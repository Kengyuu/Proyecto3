using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSubRoomController : MonoBehaviour
{
    public TextMeshProUGUI textWall;
    // Start is called before the first frame update
    public  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
            textWall.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
            textWall.gameObject.SetActive(false);
    }
}
