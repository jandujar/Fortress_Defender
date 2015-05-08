using UnityEngine;
using System.Collections;

public class ObjectStats : MonoBehaviour
{
	[SerializeField] bool goblinFaction;
	[SerializeField] int health = 100;
	public bool GoblinFaction { get { return goblinFaction; }}
	public int Health { get { return health; } set { health = value; }}

	public void Damage(int damageTotal)
	{
		Health -= damageTotal;
		
		if (Health <= 0)
			Destroy(gameObject);
	}
}
