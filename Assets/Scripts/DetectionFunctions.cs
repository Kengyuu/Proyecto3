using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static GameObject FindObjectInArea (GameObject user, string tag, float radius) {

		GameObject [] targets = GameObject.FindGameObjectsWithTag(tag);

		if (targets.Length==0) return null;

		float dist = 0;

		GameObject closest = targets[0];

		float minDistance = (closest.transform.position - user.transform.position).magnitude;

		for (int i = 1; i < targets.Length; i++) 
        {
			dist = (targets[i].transform.position - user.transform.position).magnitude;
			if (dist < minDistance) 
            {
				minDistance = dist;
				closest = targets[i];
			}
		}
        
		if (minDistance < radius) 
        {
            return closest;
        }
        else
        {
            return null;
        }
	}
    public static float DistanceToTarget (GameObject user, GameObject target) {
		return (target.transform.position - user.transform.position).magnitude;
	}
}
