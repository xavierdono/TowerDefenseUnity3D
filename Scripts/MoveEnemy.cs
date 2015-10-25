﻿using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour
{
	[HideInInspector]
	public GameObject[] waypoints;
	private int currentWaypoint = 0;
	private float lastWaypointSwitchTime;
	public float speed = 1.0f;
	private GameManagerBehavior gameManager;

	void Start ()
	{
		lastWaypointSwitchTime = Time.time;
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehavior> ();
	}

	void Update ()
	{
		Vector3 startPosition = waypoints [currentWaypoint].transform.position;
		Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;

		float pathLength = Vector3.Distance (startPosition, endPosition);
		float totalTimeForPath = pathLength / speed;
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

		if (gameObject.transform.position.Equals (endPosition)) {
			if (currentWaypoint < waypoints.Length - 2) {

				currentWaypoint++;
				lastWaypointSwitchTime = Time.time;

				RotateIntoMoveDirection ();
			} else {
				Destroy (gameObject);

				AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
				AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);

				gameManager.Health -= 1;
			}
		}
	}

	private void RotateIntoMoveDirection ()
	{
		Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
		Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;
		Vector3 newDirection = (newEndPosition - newStartPosition);

		float x = newDirection.x;
		float y = newDirection.y;
		float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;

		GameObject sprite = (GameObject)gameObject.transform.FindChild ("Sprite").gameObject;
		sprite.transform.rotation = Quaternion.AngleAxis (rotationAngle, Vector3.forward);
	}

	public float distanceToGoal ()
	{
		float distance = 0;
		distance += Vector3.Distance (gameObject.transform.position, waypoints [currentWaypoint + 1].transform.position);

		for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++) {
			Vector3 startPosition = waypoints [i].transform.position;
			Vector3 endPosition = waypoints [i + 1].transform.position;
			distance += Vector3.Distance (startPosition, endPosition);
		}

		return distance;
	}
}