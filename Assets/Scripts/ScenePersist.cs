using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    private int lastActiveScene = -1;

    private void Awake()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        // Active scene still not set
        if (lastActiveScene == -1)
            lastActiveScene = activeScene;

        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersist > 1)
        {
            if (lastActiveScene == activeScene)
            {
                Destroy(gameObject);
            }
            else
            {
                lastActiveScene = activeScene;
                List<ScenePersist> scenePersistsList = new List<ScenePersist>(FindObjectsOfType<ScenePersist>());
                scenePersistsList.Remove(this);
                foreach(ScenePersist scenePersist in scenePersistsList)
                {
                    Destroy(scenePersist);
                }
            }
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
