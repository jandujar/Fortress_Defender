using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	Vector3 oldPos;
	Vector3 panOrigin;
	float panSpeed = 80f;
	float zoomSpeed = 2f;
	bool movementRelease = true;

	void OnEnable()
	{
		EasyTouch.On_DragStart += On_DragStart;
		EasyTouch.On_Drag += On_Drag;
		EasyTouch.On_Pinch += On_Pinch;
		EasyTouch.On_PinchEnd += On_PinchEnd;
	}

	void OnDisable()
	{
		EasyTouch.On_DragStart -= On_DragStart;
		EasyTouch.On_Drag -= On_Drag;
		EasyTouch.On_Pinch -= On_Pinch;
		EasyTouch.On_PinchEnd -= On_PinchEnd;
	}

	void OnDestroy()
	{
		EasyTouch.On_DragStart -= On_DragStart;
		EasyTouch.On_Drag -= On_Drag;
		EasyTouch.On_Pinch -= On_Pinch;
		EasyTouch.On_PinchEnd -= On_PinchEnd;
	}

	void On_DragStart(Gesture gesture)
	{
		if (movementRelease && gesture.touchCount == 1)
		{
			oldPos = transform.localPosition;
			panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
	}

	void On_Drag(Gesture gesture)
	{
		if (movementRelease && gesture.touchCount == 1)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;
			transform.localPosition = new Vector3(oldPos.x + -pos.x * panSpeed, oldPos.y + -pos.y * panSpeed, transform.localPosition.z);
		}
	}

	void On_Pinch(Gesture gesture)
	{
		StopCoroutine(MovementRelease());
		movementRelease = false;
		float zoom = (Time.deltaTime * gesture.deltaPinch) * zoomSpeed;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + zoom);
	}

	void On_PinchEnd (Gesture gesture)
	{
		StartCoroutine(MovementRelease());
	}

	IEnumerator MovementRelease()
	{
		yield return new WaitForSeconds(0.1f);
		movementRelease = true;
	}
}
