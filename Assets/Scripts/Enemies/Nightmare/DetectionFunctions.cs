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
        /*Vector3 distance = new Vector3(target.transform.position.x - user.transform.position.x, 0, 
        target.transform.position.z - user.transform.position.z);*/

		 //distance = target.transform.position - user.transform.position;
		return Vector3.Distance(target.transform.position, user.transform.position);
	}

	public static bool PlayerInCone(GameObject user, GameObject target, float maxAngle, float maxRange, LayerMask mask)
	{
		Vector3 vectorEnemyPlayer = Vector3.zero;
		
	    vectorEnemyPlayer = new Vector3(target.transform.position.x - user.transform.position.x, (target.transform.position.y + 1f - user.transform.position.y), 
        	                            target.transform.position.z - user.transform.position.z);
		
		
		
		//Vector3 vectorEnemyPlayer = target.transform.position - user.transform.position;


		float angle =  Vector3.Angle(user.transform.forward, vectorEnemyPlayer);
		
		
		//Debug.DrawRay(user.transform.position, vectorEnemyPlayer.normalized * maxRange,  Color.red);
			//Debug.DrawRay(user.transform.position, user.transform.forward * maxRange, Color.blue);

		if ((angle <= maxAngle || angle >= (360 - maxAngle)) && vectorEnemyPlayer.magnitude <= maxRange)
		{
			RaycastHit hit;

			Ray Ray = new Ray(user.transform.position, vectorEnemyPlayer.normalized * maxRange);
			
			
			
			//Debug.Log(vectorEnemyPlayer.normalized * maxRange);
			if (Physics.Raycast(Ray, out hit, maxRange, mask) && GameManager.Instance.GetPlayer().GetComponent<PlayerHiddenPrayer>().m_IsPlayerVisibleToEnemy)
			{
				//Debug.Log(angle);
				/*Debug.DrawRay(user.transform.position, user.transform.forward * maxRange, Color.blue);*/
				//Debug.Log("Distancia hit: " + hit.distance + " Distancia Player: " + vectorEnemyPlayer.magnitude);
				//Debug.Log(hit.collider.transform.name);
				/*if(Mathf.Abs(hit.distance - vectorEnemyPlayer.magnitude) < 2)
				{
					Debug.Log("Estoy dentro");
				}*/
				if (hit.collider.CompareTag("Player"))
				{
					Debug.DrawRay(user.transform.position, vectorEnemyPlayer.normalized * maxRange,  Color.red);
					//user.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
					//Debug.Log(angle + " Estoy IN");
					//Debug.Log(angle + " MaxAngle: " + maxAngle);
					return true;
				}
				else
				{
					//Debug.Log(maxAngle + " Estoy OUT");
					//Debug.Log(hit.collider.name);
					Debug.DrawRay(user.transform.position, vectorEnemyPlayer.normalized * maxRange, Color.green);
					//user.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
				}
			}
			

		}
		return false;
		//Debug.Log("SEES PLAYER");
		
	}


}
