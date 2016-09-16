﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    public int ballValue;

    private int score;

	// Use this for initialization
	void Start () {
        score = 0;
        UpdateScore();
	}
	
    void OnTriggerEnter2D(Collider2D collider)
    {
        score += ballValue;
        UpdateScore();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            score -= ballValue * 2;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score:\n" + score;
    }
}
