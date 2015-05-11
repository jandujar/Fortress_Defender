using UnityEngine;
using System.Collections;

public class DefenseTargeting : MonoBehaviour
{
	[SerializeField] bool goblinFaction;
	[SerializeField] float range;
	[SerializeField] float lookSpeed;
	float closestDistance;
	Transform target;
	public Transform Target { get { return target;}}

	void Start()
	{
		InvokeRepeating("FindClosestTarget", 0f, 0.2f);
	}
	
	void FindClosestTarget()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

		if (hitColliders.Length > 0)
		{
			closestDistance = 100f;

			foreach (Collider potentialTarget in hitColliders)
			{
				ObjectStats objectStats = potentialTarget.GetComponent<ObjectStats>();

				if (objectStats != null && objectStats.GoblinFaction != goblinFaction)
				{
					float targetDistance = Vector3.Distance(transform.position, potentialTarget.transform.position);

					if (targetDistance < closestDistance)
					{
						closestDistance = targetDistance;
						target = potentialTarget.transform;
					}
				}
			}
		}
	}

	void Update()
	{
		if (target)
		{
			var targetPos = target.position;
			targetPos.y = transform.position.y;
			var targetDir = Quaternion.LookRotation(targetPos - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetDir, lookSpeed * Time.deltaTime);
		}
	}
}
