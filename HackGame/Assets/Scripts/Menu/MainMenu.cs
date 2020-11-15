using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject audioManager;
	public void PlayGame ()
	{
		GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneTransition>().NextScene();
		StartCoroutine(PlayStartAudio());
	}

	public void QuitGame ()
	{
		Debug.Log("QUIT");
		Application.Quit();
	}

	private IEnumerator PlayStartAudio()
    {
		yield return new WaitForSeconds(1);

		FindObjectOfType<AudioManager>().Stop("menu clip");
		FindObjectOfType<AudioManager>().Play("guitarMusic clip");
	}
}
