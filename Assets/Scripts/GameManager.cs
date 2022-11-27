using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    [SerializeField] public Player playerObj;

    [SerializeField] private int playerHits;
    [SerializeField] private int playerHitAttempts;

    [SerializeField] private int playerThrows;

    [SerializeField] public GameObject enemy;
    [SerializeField] private Transform spawnPos;

    private GameObject currentEnemy;

    private void Awake()
    {
        _instance = this;
    }

    public void SetPlayer(Player p)
    {
        playerObj = p;
    }

    public void HittedBall()
    {
        playerHitAttempts++;

        if (playerHitAttempts >= 3)
        {
            InputManager.Instance.DisableWelcomeMessage();

            if (currentEnemy == null)
            {
                currentEnemy = Instantiate(enemy, spawnPos.transform.position, Quaternion.identity);
                currentEnemy.transform.eulerAngles = new Vector3(
                    currentEnemy.transform.eulerAngles.x,
                    currentEnemy.transform.eulerAngles.y + 180,
                    currentEnemy.transform.eulerAngles.z);
            }
        }
    }

    public void SuccessfullHit()
    {
        if (playerHits < 3)
        {
            InputManager.Instance.ColorizeBall(playerHits);

        } 

        InputManager.Instance.SwitchTextForHit(true);
        playerHits++;

        if (playerHits >= 3)
        {
            SwitchToThrowTraining();
        }
    }

    public void SuccessfullThrow()
    {
        if (playerThrows < 3)
        {
            InputManager.Instance.ColorizeBall(playerThrows);
        }

        playerThrows++;
    }

    public void SwitchToThrowTraining()
    {
        InputManager.Instance.SwitchToThrowTraining();
        currentEnemy.SetActive(false);
    }

    public void SwitchToEnemy()
    {
        if (!currentEnemy.activeSelf)
        {
            currentEnemy.SetActive(true);
            currentEnemy.GetComponent<Enemy>().isThrowing = false;
        }
    }


}
