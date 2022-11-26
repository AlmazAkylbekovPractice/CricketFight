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

    [SerializeField] private Player playerObj;

    [SerializeField] private int playerHits;
    [SerializeField] private int playerHitAttempts;


    [SerializeField] private GameObject enemy;
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

}
