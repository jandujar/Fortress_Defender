using UnityEngine;
using System.Collections;

public class UnitAttack : MonoBehaviour
{
	[SerializeField] UnitController unitController;
	[SerializeField] ObjectStats objectStats;
	
	void OnTriggerEnter(Collider other)
	{
		ObjectStats enemyStats = other.GetComponent<ObjectStats>();
		
		if (enemyStats != null && objectStats.GoblinFaction != enemyStats.GoblinFaction)
		{
			unitController.AttackObject = other.gameObject;
			unitController.state = UnitController.State.Attack;
		}
	}
}
