using UnityEngine;
using System.Collections;

public class DefenseBallista : MonoBehaviour
{
	[SerializeField] DefenseTargeting defenseTargeting;
	[SerializeField] Transform boltPosition;
	[SerializeField] GameObject bolt;
	[SerializeField] float reloadTime;
	bool canShoot = true;

	void Start()
	{
		InvokeRepeating("FirePrecheck", 0f, 0.2f);
	}

	void FirePrecheck()
	{
		if (defenseTargeting.Target != null && canShoot)
			StartCoroutine(Fire());
	}

	IEnumerator Fire()
	{
		canShoot = false;
		GameObject boltClone = (GameObject)Instantiate(bolt, boltPosition.position, transform.rotation);
		DefenseBallistaBolt ballistaBolt = boltClone.GetComponentInChildren<DefenseBallistaBolt>();
		ballistaBolt.Target = defenseTargeting.Target;
		ballistaBolt.TargetAcquired = true;
		yield return new WaitForSeconds(reloadTime);
		canShoot = true;
	}
}
