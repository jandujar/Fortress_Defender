using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour
{
	[SerializeField] NavMeshAgent agent;
	[SerializeField] bool goblinUnit;
	[SerializeField] int laneNumber;
	GameObject targetObject;
	Transform target;
	State currentState;
	ObjectStats attackStats;
	GameObject attackObject;
	public GameObject AttackObject { get { return attackObject; } set { attackObject = value; }}

	public enum State
	{
		Travel,
		Attack,
		Death
	}

	public State state;

	void Start()
	{
		switch(laneNumber)
		{
		case 1:
			targetObject = GameObject.FindGameObjectWithTag("Lane1");
			break;
		case 2:
			targetObject = GameObject.FindGameObjectWithTag("Lane2");
			break;
		case 3:
			targetObject = GameObject.FindGameObjectWithTag("Lane3");
			break;
		default:
			Debug.LogError("No Lane Specified");
			break;
		}

		target = targetObject.transform;
		state = State.Travel;
		StartCoroutine(TravelState());
	}

	void Update()
	{
		if (currentState != state)
		{
			currentState = state;

			switch(state)
			{
			case State.Attack:
				StartCoroutine(AttackState());
				break;
			case State.Death:
				DeathState();
				break;
			default:
				StartCoroutine(TravelState());
				break;
			}
		}
	}

	IEnumerator TravelState()
	{
		agent.destination = target.position;

		while (state == State.Travel)
		{
			if (Vector3.Distance(transform.position, target.position) <= 10f)
			{
				if (goblinUnit)
				{
					targetObject = GameObject.FindGameObjectWithTag("AllyGoal");
				}
				else
					targetObject = GameObject.FindGameObjectWithTag("EnemyGoal");
				
				target = targetObject.transform;
				agent.destination = target.position;
				break;
			}

			yield return 0;
		}
	}
	
	IEnumerator AttackState()
	{
		agent.destination = transform.position;
		attackStats = AttackObject.GetComponent<ObjectStats>();
		InvokeRepeating("Attack", 0f, 1f);

		while (state == State.Attack)
		{
			if (AttackObject == null)
			{
				CancelInvoke("Attack");
				state = State.Travel;
			}

			yield return 0;
		}
	}

	void Attack()
	{
		if (attackStats != null)
			attackStats.Damage(20);
	}

	void DeathState()
	{
		Destroy(gameObject, 1f);
	}
}
