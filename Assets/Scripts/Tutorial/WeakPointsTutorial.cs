using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointsTutorial : MonoBehaviour
{
    public WPControllerTutorial wpController;
 
    public void DestroyWP()
    {
        wpController.wpLeft--;
        wpController.TutorialControl();
        gameObject.SetActive(false);
    }
}
