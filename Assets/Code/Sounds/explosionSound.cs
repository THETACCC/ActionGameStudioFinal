using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionSound : MonoBehaviour
{

    //audio
    public AudioClip explosion;
    private AudioSource audioSource;

    private bool audioplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioplayed == false)
        {
            audioSource.PlayOneShot(explosion);
            audioplayed = true;
            StartCoroutine(SelfDestruct());
        }
    }

    private IEnumerator SelfDestruct()
    {

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
