using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	void RestartLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}