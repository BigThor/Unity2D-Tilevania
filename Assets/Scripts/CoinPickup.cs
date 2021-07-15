using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    AudioSource source;
    private bool alreadyTouched = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (alreadyTouched)
            return;

        alreadyTouched = true;

        FindObjectOfType<AudioManager>().PlaySound("CoinPickup");
        FindObjectOfType<GameSession>().AddOneToCoinCount();
        Destroy(this.gameObject);
    }
}
