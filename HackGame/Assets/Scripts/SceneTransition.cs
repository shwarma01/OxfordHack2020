using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1f;

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(Reset());
        }
    }

    public void NextScene() {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadScene(int buildIndex)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        FindObjectOfType<AudioManager>().StopAll();
        SceneManager.LoadScene(buildIndex);
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(6);
        
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().Play("menu clip");
    }
}
