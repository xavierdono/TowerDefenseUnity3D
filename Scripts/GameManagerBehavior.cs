﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerBehavior : MonoBehaviour
{
	public Text goldLabel;
	public Text waveLabel;
	public Text healthLabel;
	public GameObject[] healthIndicator;
	public GameObject[] nextWaveLabels;
	public bool gameOver = false;

	private int gold;

	public int Gold {
		get { return gold; }
		set {
			gold = value;
			goldLabel.GetComponent<Text> ().text = "GOLD : " + gold;
		}
	}

	private int wave;

	public int Wave {
		get { return wave; }
		set {
			wave = value;

			if (!gameOver) {
				for (int i = 0; i < nextWaveLabels.Length; i++) {
					nextWaveLabels [i].GetComponent<Animator> ().SetTrigger ("nextWave");
				}
			}

			waveLabel.text = "WAVE : " + (wave + 1);
		}
	}

	private int health;

	public int Health {
		get { return health; }
		set {
			if (value < health) {
				Camera.main.GetComponent<CameraShake> ().Shake ();
			}

			health = value;
			healthLabel.text = "HEALTH : " + health;

			if (health <= 0 && !gameOver) {
				gameOver = true;
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameOver");
				gameOverText.GetComponent<Animator> ().SetBool ("gameOver", true);
			}

			for (int i = 0; i < healthIndicator.Length; i++) {
				if (i < Health) {
					healthIndicator [i].SetActive (true);
				} else {
					healthIndicator [i].SetActive (false);
				}
			}
		}
	}

	void Start ()
	{
		Gold = 1000;
		Wave = 0;
		Health = 5;
	}
}