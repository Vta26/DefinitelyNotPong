using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Effect : MonoBehaviour
{
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            source.Play();
        }
    }
}
