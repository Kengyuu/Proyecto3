using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionFunctions : MonoBehaviour
{

    public static GameObject FindObjectInArea (GameObject user, string tag, float radius) {

		GameObject [] targets = GameObject.FindGameObjectsWithTag(tag);

		if (targets.Length==0) return null;

		float dist = 0;

		GameObject closest = targets[0];

		//float minDistance = (closest.transform.position - user.transform.position).magnitude;
		float minDistance = new Vector3(closest.transform.position.x - user.transform.position.x , 0, closest.transform.position.z - user.transform.position.z).magnitude;

		for (int i = 1; i < targets.Length; i++) 
        {
			//dist = (targets[i].transform.position - user.transform.position).magnitude;
			dist = new Vector3(targets[i].transform.position.x - user.transform.position.x , 0, targets[i].transform.position.z - user.transform.position.z).magnitude;
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

	public static List<GameObject> FindObjectsInArea(GameObject user, string tag, float radius)
	{
		GameObject [] targets = GameObject.FindGameObjectsWithTag(tag);
		List<GameObject> targetsList = new List<GameObject>();
		
		foreach(GameObject t in targets)
		{
			if(DistanceToTarget(user, t) <= radius)
			{
				targetsList.Add(t);
			}
		}
		if (targetsList.Count == 0) return null;
		
		return targetsList;
	}
    public static float DistanceToTarget (GameObject user, GameObject target) 
    {
        Vector3 distance = new Vector3(target.transform.position.x - user.transform.position.x, 0, 
        target.transform.position.z - user.transform.position.z);
		return (distance).magnitude;
	}

	public static bool PlayerInCone(GameObject user, GameObject target, float maxAngle, float maxRange, LayerMask layer)
	{
		/*Vector3 vectorEnemyPlayer = new Vector3(target.transform.position.x - user.transform.position.x, 0, 
        target.transform.position.z - user.transform.position.z);*/
		Vector3 vectorEnemyPlayer = target.transform.position - user.transform.position;
		float angle =  Vector3.Angle(user.transform.forward, vectorEnemyPlayer);
		
		RaycastHit hit;
			
		
		if (angle <= maxAngle && vectorEnemyPlayer.magnitude <= maxRange)
		{
			Ray Ray = new Ray(user.transform.position, vectorEnemyPlayer.normalized * maxRange);
			Debug.DrawRay(user.transform.position, vectorEnemyPlayer.normalized * maxRange, Color.red);
			
			/*if (hit.collider.tag != "Map")
			{*/
			if (Physics.Raycast(Ray, out hit, maxRange))
			{
				Debug.Log(hit.transform.name);
				if(hit.transform.tag == "Player")
				{
					user.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
					Debug.Log(angle + " Estoy IN");
					return true;
				}
			}
		}
		else
		{
			Debug.DrawRay(user.transform.position, vectorEnemyPlayer.normalized * maxRange, Color.green);
			user.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
		}
		
		
		return false;
	}


}
