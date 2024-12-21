using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float _speed = 5.0f;
    public float x_value = -1.0f;
    public float y_value = 1.0f;
    public int scored = 0;
    private int score_left = 0;
    private int score_right = 0;
    private bool game_over = false;
    public TextMeshProUGUI _text;
    public TextMeshProUGUI _winner_text;
    void Start()
    {
        y_value = Random.Range(-1.0f,1.0f);
        _text.text = score_left.ToString() + ":" + score_right.ToString();
        print("Game Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (scored != 0 && Input.GetKeyDown(KeyCode.Return))
        {
            y_value = Random.Range(-1.0f,1.0f);
            if (scored < 0)
            {
                x_value = -1.0f;
            }
            else{
                x_value = 1.0f;
            }
            scored = 0;
            if (game_over)
            {
                score_left = 0;
                score_right = 0;
                _text.text = score_left.ToString() + ":" + score_right.ToString();
                game_over = false;
                _winner_text.text = "";
            }
        }
        Vector3 move = new Vector3(x_value, y_value, 0);
        transform.position += move * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "KillZone")
        {
            x_value = 0;
            y_value = 0;
            if (transform.position.x>0){
                scored = 1;
                score_left++;
            }
            else
            {
                scored = -1;
                score_right++;
            }
            transform.position = new Vector3(0,0,0);
            _speed = 5.0f;
            _text.text = score_left.ToString() + ":" + score_right.ToString();
            if (score_left >= 10)
            {
                _winner_text.text = "Player 1 Wins!";
                game_over = true;
            }
            if (score_right >= 10)
            {
                _winner_text.text = "Player 2 Wins!";
                game_over = true;
            }
            print("Press Enter to continue");
            //if enter is pressed, randomize between y = 1 and y = -1, and set x to whichever side got scored on
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision Detected");
        if (collision.gameObject.tag == "Player")
        {
            print("Player Collision");
            x_value = -x_value;
            if (y_value > 0)
            {
                y_value = Random.Range(0.0f,1.0f);
            }
            else
            {
                y_value = Random.Range(-1.0f,0.0f);
            }
            _speed = _speed + 0.2f;
        }

        if (collision.gameObject.tag == "Wall")
        {
            print("Wall Collision");
            y_value = -y_value;
        }
    }
}
