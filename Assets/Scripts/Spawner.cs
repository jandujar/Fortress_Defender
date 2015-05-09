using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject unitPrefab;
	int unitNumber = 1;

	void Start()
	{
		StartCoroutine(InitialSpawnWait());
	}

	IEnumerator InitialSpawnWait()
	{
		yield return new WaitForSeconds(5f);
		StartCoroutine(SpawnUnit());
	}

	IEnumerator SpawnUnit()
	{
		int i = 0;

		while (i < 4)
		{
			float randomX = Random.Range(transform.position.x - 1f, transform.position.x + 1f);
			float randomZ = Random.Range(transform.position.z - 1f, transform.position.z + 1f);
			var clone = Instantiate(unitPrefab, new Vector3(randomX, transform.position.y, randomZ), transform.rotation);
			clone.name = "Unit_" + unitNumber;
			unitNumber++;
			i++;
			yield return new WaitForSeconds(0.5f);
		}

		yield return new WaitForSeconds(10f);
		StartCoroutine(SpawnUnit());
	}
}
