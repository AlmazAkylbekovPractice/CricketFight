using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController enemyController;
    private Animator enemyAnim;

    private GameObject player;

    private string IS_THROWING_TAG = "isThrowing";

    // Start is called before the first frame update
    void Start()
    {
        SetComponents();
    }

    void SetComponents()
    {
        enemyController = GetComponent<CharacterController>();
        enemyAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void FixedUpdate()
    {
        if (player.GetComponent<Player>().isReady)
        {
            ThrowBall();
        }
    }

    private void ThrowBall()
    {
        enemyAnim.SetBool(IS_THROWING_TAG, true);
    }
}
