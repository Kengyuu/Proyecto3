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

		float minDistance = new Vector3(closest.transform.position.x - user.transform.position.x , 0, closest.transform.position.z - user.transform.position.z).magnitude;

		for (int i = 1; i < targets.Length; i++) 
        {
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
		return Vector3.Distance(target.transform.position, user.transform.position);
	}

	public static bool PlayerInCone(GameObject user, GameObject target, float maxAngle, float maxRange, LayerMask mask)
	{
		Vector3 vectorEnemyPlayer = Vector3.zero;
		
	    vectorEnemyPlayer = new Vector3(target.transform.position.x - user.transform.position.x, (target.transform.position.y + 1f - user.transform.position.y), 
        	                            target.transform.position.z - user.transform.position.z);
		
		float angle =  Vector3.Angle(user.transform.forward, vectorEnemyPlayer);
		
		

		if ((angle <= maxAngle || angle >= (360 - maxAngle)) && vectorEnemyPlayer.magnitude <= maxRange)
		{
			RaycastHit hit;

			Ray Ray = new Ray(user.transform.position, vectorEnemyPlayer.normalized * maxRange);
			
			if (Physics.Raycast(Ray, out hit, maxRange, mask) && GameManager.Instance.GetPlayer().GetComponent<PlayerSpecialAbilities>().m_IsPlayerVisibleToEnemy)
			{
				
				if (hit.collider.CompareTag("Player"))
				{
					return true;
				}
			}
			

		}
		return false;	
	}


}
