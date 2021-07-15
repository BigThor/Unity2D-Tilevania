using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 3.0f;
    [SerializeField] GameObject FireworksPrefab;


    private PolygonCollider2D _doorCollider;

    void Start()
    {
        _doorCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 doorPosition = _doorCollider.transform.position;
        Vector3 fireworksPosition = new Vector3(
            (float)(doorPosition.x - 0.3),
            (float)(doorPosition.y + 1.3), 
            doorPosition.z
           );
        Instantiate(FireworksPrefab, fireworksPosition, Quaternion.identity);
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((currentSceneIndex+1) % SceneManager.sceneCountInBuildSettings);
    }
}
