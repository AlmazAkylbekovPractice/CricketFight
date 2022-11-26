using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    #region Singleton
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    [SerializeField] private Player playerObj;

    [SerializeField] private Button hitButton;
    [SerializeField] private Button throwButton;
    [SerializeField] private Text welcomeMessage;
    [SerializeField] private Text hitBallsMessage;

    private void Awake()
    {
        _instance = this;

        AssignButtons();
        HighlightButton();
    }

    public void SetPlayer(Player p)
    {
        playerObj = p;
    }

    private void AssignButtons()
    {
        if (hitButton.enabled)
            hitButton.onClick.AddListener(HitBall);

        if (throwButton.enabled)
            throwButton.onClick.AddListener(ThrowBall);
    }

    private void HitBall()
    {
        playerObj.SetBehaviorHit();
    }

    private void ThrowBall()
    {
        playerObj.SetBehaviorThrow();
    }

    private void HighlightButton()
    {
        hitButton.Select();
    }

    public void DisableWelcomeMessage()
    {
        if (welcomeMessage.enabled)
            welcomeMessage.enabled = false;

        if (!hitBallsMessage.enabled)
            hitBallsMessage.enabled = true;
    }

}
