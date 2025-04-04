using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private float _speed;
    public GameObject ball;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
    }

    void Update()
    {
        _speed = ball.GetComponent<BallScript>()._speed*2;
        if ((Input.GetAxisRaw("Vertical") == -1 && transform.position.y <= -4.185)|(Input.GetAxisRaw("Vertical") == 1 && transform.position.y >= 4.185))
        {
            return;
        }
        Vector3 move = new Vector3(0,Input.GetAxisRaw("Vertical"),0);
        transform.position += move * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (source == null){
                source = GetComponent<AudioSource>();
            }
            source.Play();
        }
    }
}
