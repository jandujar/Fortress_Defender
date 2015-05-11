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
		float initialSpawnWait = Random.Range(1f, 6f);
		yield return new WaitForSeconds(initialSpawnWait);
		StartCoroutine(SpawnUnit());
	}

	IEnumerator SpawnUnit()
	{
		int i = 0;
		int randomSpawnNumber = Random.Range(1, 5);

		while (i < randomSpawnNumber)
		{
			float randomX = Random.Range(transform.position.x - 1f, transform.position.x + 1f);
			float randomZ = Random.Range(transform.position.z - 1f, transform.position.z + 1f);
			var clone = Instantiate(unitPrefab, new Vector3(randomX, transform.position.y, randomZ), transform.rotation);
			clone.name = "Unit_" + unitNumber;
			unitNumber++;
			i++;
			float randomSpawnWait = Random.Range(0.4f, 0.6f);
			yield return new WaitForSeconds(randomSpawnWait);
		}

		float randomWait = Random.Range(8f, 12f);
		yield return new WaitForSeconds(randomWait);
		StartCoroutine(SpawnUnit());
	}
}
