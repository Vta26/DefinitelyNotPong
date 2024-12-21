using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float _speed = 10.0f;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
}
