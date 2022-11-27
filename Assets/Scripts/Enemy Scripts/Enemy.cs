using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController enemyController;
    private Animator enemyAnim;

    private GameObject player;

    [SerializeField] private Transform appleSpawnPlace;
    [SerializeField] private GameObject ballPrefab;

    private string IS_THROWING_TAG = "isThrowing";

    private GameObject currentBall;

    [SerializeField] private float throwTimer = 2.15f;

    public bool isThrowing = true;

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
        if (isThrowing)
        {
            ThrowBall();
        }
        else
        {
            HitBall();
        }
    }

    private void ThrowBall()
    {
        enemyAnim.Play("Throw");

        float time = enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (time >= 0.46f)
        {
            if (currentBall == null)
            {
                currentBall = Instantiate(ballPrefab, appleSpawnPlace.transform.position, Quaternion.identity);
                currentBall.GetComponent<Ball>().byEnemy = true;
            }
        } else if (time >= 0.1f && time < 0.3f)
        {
            currentBall = null;
        }
    }

    private void HitBall()
    {
        enemyAnim.Play("Hit");
    }

    
}
