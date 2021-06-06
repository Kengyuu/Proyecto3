using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointsTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public WPControllerTutorial wpController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyWP()
    {
        wpController.wpLeft--;
        wpController.TutorialControl();
        gameObject.SetActive(false);
    }
}
