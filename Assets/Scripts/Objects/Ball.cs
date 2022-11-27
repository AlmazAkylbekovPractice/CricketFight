using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SphereCollider ballCollider;
    private Rigidbody ballBody;

    [SerializeField] private GameObject targetPlayer;

    [SerializeField] private float force;

    private bool isHitted;

    [SerializeField] public bool byEnemy;

    private float timer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        LoadComponents();
        LaunchBall();
    }

    private void LoadComponents()
    {
        ballBody = GetComponent<Rigidbody>();
        ballCollider = GetComponent<SphereCollider>();

        ballCollider.isTrigger = true;

        if (byEnemy)
            targetPlayer = GameObject.FindGameObjectWithTag("PlayerTarget");
        else targetPlayer = GameObject.FindGameObjectWithTag("EnemyTarget");
    }

    private void FixedUpdate()
    {
        if (!isHitted)
            LaunchBall();

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            ballCollider.isTrigger = false;
        }
    }

    public void LaunchBall()
    {
        ballBody.AddForce((targetPlayer.transform.position - transform.position).normalized * force * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (byEnemy)
        {
            if (collision.gameObject.tag == "Sword")
            {
                ballBody.useGravity = true;
                isHitted = true;

                Vector3 dir = collision.contacts[0].point - transform.position;
                dir = -dir.normalized;

                ballBody.AddForce(new Vector3(
                    targetPlayer.transform.position.x,
                    targetPlayer.transform.position.y,
                    -targetPlayer.transform.position.z) * force * 3f * Time.deltaTime);

                GameManager.Instance.SuccessfullHit();
            }

            if (collision.gameObject.tag == "PlayerTarget")
            {
                InputManager.Instance.SwitchTextForHit(false);
            }
        }
        else
        {
            if (collision.gameObject.tag == "EnemySword")
            {
                ballBody.useGravity = true;
                isHitted = true;

                Vector3 dir = collision.contacts[0].point - transform.position;
                dir = -dir.normalized;

                ballBody.AddForce(new Vector3(
                    targetPlayer.transform.position.x,
                    targetPlayer.transform.position.y,
                    targetPlayer.transform.position.z) * force * 3f * Time.deltaTime);
            }

            if (collision.gameObject.tag == "EnemyTarget")
            {
                GameManager.Instance.SuccessfullThrow();
                InputManager.Instance.SwitchTextForHit(false);
            }
        }
    }
}
