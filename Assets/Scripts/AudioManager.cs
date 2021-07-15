using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource coinPickup;

    private void Awake()
    {
        int numAudioManagers = FindObjectsOfType<AudioManager>().Length;

        if (numAudioManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        coinPickup = audioSources[0];
    }

    public void PlaySound(string sound)
    {
        switch(sound)
        {
            case "CoinPickup":
                coinPickup.Play();
                break;
        }
    }
}
