using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
	[SerializeField] TransitionManager manager;
	public int sceneNumber;
	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
			manager.GoToScene(sceneNumber);
        }
	}
}
