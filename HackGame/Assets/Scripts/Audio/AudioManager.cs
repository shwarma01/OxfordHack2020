using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    /* --------------------------------------------------------------------------------------------
     * Usage:
     * In the place where you want to play the sound write
     * FindObjectOfType<AudioManager>().Play("Clip 1, Clip 2, Clip 3, ..., death clip (pick one)")
     * 
     * What if you want the audio manager in multiple scenes?
     * Put the game object into the prefab folder and then drag it from there to the scenes you want
     * The DontDestory... handles the audio not cutting off when switching scenes
     * And the if(instance...) makes sure there aren't multiple audio managers
     * ----------------------------------------------------------------------------------------------*/

    public Sound[] sounds;

    public static AudioManager instance;

    private bool gameOver = false;

    private List<Sound> allSources = new List<Sound>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            allSources.Add(s);
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Play a sound at the start of the game
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Play("menu clip");
        }        
    }

    // Playing a sound
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Can't find " + name);
        }
        else if (s.source.isPlaying == false)
        {
            s.source.Play();
        }
    }

    // Playing a sound
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Can't find " + name);
        }
        else
        {
            s.source.Stop();
        }
    }

    public void StopAll()
    {
        foreach (Sound s in allSources)
        {
            s.source.Stop();
        }
    }

    public void GameOver()
    {
        if (gameOver == false)
        {
            StartCoroutine(PlayEndingClip(1f));
            gameOver = true;
        }
    }

    private IEnumerator PlayEndingClip(float waitAmt)
    {
        yield return new WaitForSeconds(waitAmt);
        Play("death clip");
        gameOver = false;
    }
}
