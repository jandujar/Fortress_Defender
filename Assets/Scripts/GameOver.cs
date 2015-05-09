using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	[SerializeField] bool victory;

	void OnTriggerEnter(Collider other)
	{
		if (victory)
			Debug.Log("Victory");
		else
			Debug.Log("Defeat");
	}
}
