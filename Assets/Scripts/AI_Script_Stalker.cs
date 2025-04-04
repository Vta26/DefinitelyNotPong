using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI_Script_Stalker : MonoBehaviour
{
    public float _speed = 10.0f;
    public GameObject ball;
    private int y_move;
    public int style = 1;
    private double target;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
    }

    void TypeChange()
    {
        style = Random.Range(0,3);
        switch (style)
        {
            case 0://type 0; Player has a chance
                Debug.Log("Casual Style");
                break;
            case 1://type 1; Follow the ball exactly
                Debug.Log("Stalker Style");
                break;
            case 2://type 2; Predict ball path
                Debug.Log("Predict Style");
                target = 0;
                break;
        }
    }

    void Update()
    {
        _speed = ball.GetComponent<BallScript>()._speed*2;

        //Update target place is ball needs to be tracked
        if (style == 1 || style == 0)
        {
            target = ball.transform.position.y;
        }
        if (style == 2)
        {
            if (ball.GetComponent<BallScript>().target_x*this.transform.position.x>0)
            {
                target = ball.GetComponent<BallScript>().target_y;
            }
        }

        if (target > this.transform.position.y)
        {
            y_move = 1;
        }
        if (target < this.transform.position.y)
        {
            y_move = -1;
        }
        if ((y_move == -1 && transform.position.y <= -4.185)|(y_move == 1 && transform.position.y >= 4.185))
        {
            y_move = 0;
            return;
        } 
        Vector3 move = new Vector3(0,y_move,0);
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
            TypeChange();
        }
    }
}
