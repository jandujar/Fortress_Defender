using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject unitPrefab;
	int unitNumber = 1;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
			SpawnUnit();
	}

	void SpawnUnit()
	{
		float randomX = Random.Range(transform.position.x - 1f, transform.position.x + 1f);
		float randomZ = Random.Range(transform.position.z - 1f, transform.position.z + 1f);
		var clone = Instantiate(unitPrefab, new Vector3(randomX, transform.position.y, randomZ), transform.rotation);
		clone.name = "Unit_" + unitNumber;
		unitNumber++;
	}
}
