using UnityEngine;
using System.Collections;

public class DefenseBallistaBolt : MonoBehaviour
{
	[SerializeField] bool goblinFaction;
	[SerializeField] float speed = 1f;
	[SerializeField] int damage = 50;
	Transform target;
	bool targetAcquired = false;
	public Transform Target { get { return target;} set { target = value; }}
	public bool TargetAcquired { get { return targetAcquired;} set { targetAcquired = value; }}

	void Update()
	{
		if (TargetAcquired)
		{
			float step = speed * Time.deltaTime;

			if (Target != null)
			{
				transform.parent.LookAt(Target.position);
				transform.position = Vector3.MoveTowards(transform.position, Target.position, step);
			}
			else
				Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		ObjectStats objectStats = other.GetComponent<ObjectStats>();

		if (objectStats != null && objectStats.GoblinFaction != goblinFaction)
		{
			objectStats.Damage(damage);
			Destroy(gameObject);
		}
	}
}
