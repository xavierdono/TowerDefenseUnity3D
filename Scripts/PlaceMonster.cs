using UnityEngine;
using System.Collections;

public class PlaceMonster : MonoBehaviour
{
	private GameManagerBehavior gameManager;
	public GameObject monsterPrefab;
	private GameObject monster;

	void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehavior> ();
	}

	private bool canPlaceMonster ()
	{
		int cost = monsterPrefab.GetComponent<MonsterData> ().levels [0].cost;
		return monster == null && gameManager.Gold >= cost;
	}

	// Automatically calls OnMouseUp when a player taps a GameObject’s physics collider
	void OnMouseUp ()
	{
		// Place free?
		if (canPlaceMonster ()) { // Yes
			
			// Store the instance of the monsterPrefab who has been clicked
			monster = (GameObject)Instantiate (monsterPrefab, transform.position, Quaternion.identity);

			// Play the sound when clicked
			AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
			audioSource.PlayOneShot (audioSource.clip);

			// Deduct gold
			gameManager.Gold -= monster.GetComponent<MonsterData> ().CurrentLevel.cost;

		} else if (canUpgradeMonster ()) {
			monster.GetComponent<MonsterData> ().increaseLevel ();

			AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
			audioSource.PlayOneShot (audioSource.clip);

			// Deduct gold
			gameManager.Gold -= monster.GetComponent<MonsterData> ().CurrentLevel.cost;

		}
	}

	private bool canUpgradeMonster ()
	{
		if (monster != null) {
			
			MonsterData monsterData = monster.GetComponent<MonsterData> ();
			MonsterLevel nextLevel = monsterData.getNextLevel ();

			if (nextLevel != null) {
				return gameManager.Gold >= nextLevel.cost;
			}
		}
		return false;
	}
}