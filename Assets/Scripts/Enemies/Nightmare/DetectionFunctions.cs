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

	public static bool PlayerInCone(GameObject user, GameObject target, float maxAngle, float maxRange, LayerMask mask)
	{
		/*Vector3 vectorEnemyPlayer = new Vector3(target.transform.position.x - user.transform.position.x, 0, 
        target.transform.position.z - user.transform.position.z);*/
		Vector3 vectorEnemyPlayer = target.transform.position - user.transform.position;


		float angle =  Vector3.Angle(user.transform.forward, vectorEnemyPlayer);
		
		
		

		if (Mathf.Abs(angle) <= Mathf.Abs(maxAngle) && vectorEnemyPlayer.magnitude <= maxRange)
		{
			RaycastHit hit;

			Ray Ray = new Ray(user.transform.position, user.transform.forward * maxRange);
			Debug.DrawRay(user.transform.position, vectorEnemyPlayer.normalized * maxRange, Color.red);
			//Debug.Log(vectorEnemyPlayer.normalized * maxRange);
			if (Physics.Raycast(Ray, out hit, maxRange, mask))
			{
				Debug.Log(hit.transform.name);
				if (hit.collider.CompareTag("Player"))
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





		//Debug.Log("SEES PLAYER");
		/*Vector3 l_Direction = (target.transform.position + Vector3.up * -0.1f) - user.transform.position;
		float l_DistanceToPlayer = l_Direction.magnitude;

		l_Direction /= l_DistanceToPlayer;

		if (l_DistanceToPlayer >= maxRange)
        {
			user.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
			return false;
		}
			

		bool l_IsOnCone = Vector3.Dot(user.transform.forward, l_Direction) >= Mathf.Cos(maxAngle * Mathf.Deg2Rad * 0.5f);
		Ray l_Ray = new Ray(user.transform.position, l_Direction);

		
		//Debug.Log(!Physics.Raycast(l_Ray, l_DistanceToPlayer, m_SightLayerMask));
		Debug.DrawRay(user.transform.position, l_Direction * l_DistanceToPlayer, Color.blue);

		if (l_IsOnCone && Physics.Raycast(l_Ray, l_DistanceToPlayer, mask))
		{
			Debug.Log(Vector3.Dot(user.transform.forward, l_Direction));
			user.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
			//Debug.Log("SEES PLAYER - ESTA EN EL CONO, PERO EL RAYCAST NO LE VE.");
			return true;
		}
		user.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
		//Debug.Log("SEES PLAYER - ESTA EN EL CONO");
		return false;*/
	}


}
