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
    [SerializeField] private Text tryAgainMessage;

    [SerializeField] private Image[] ballsSprites;
    [SerializeField] private Sprite hitedBallSprites;
    [SerializeField] private Sprite emptyBallSprite;

    [SerializeField] private GameObject aimImage;
    [SerializeField] private GameObject targetImage;

    [SerializeField] private GameObject powerSlider;

    [SerializeField] private GameObject scoresPanel;

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
        if (powerSlider.activeSelf)
        {
            MeasurePower();
        }
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

        if (!scoresPanel.activeSelf)
            scoresPanel.SetActive(true);

        if (!tryAgainMessage.enabled)
            tryAgainMessage.enabled = true;
    }

    public void SwitchTextForHit(bool hitted)
    {
        if (hitted)
        {
            tryAgainMessage.text = "Good Job!!!";
        } else
        {
            tryAgainMessage.text = "Try Again!!!";
        }
    }

    public void ColorizeBall(int index)
    {
        ballsSprites[index].GetComponent<Image>().sprite = hitedBallSprites;
    }

    public void SwitchToThrowTraining()
    {
        UpdateBalls();

        if (hitButton.gameObject.activeSelf)
            hitButton.gameObject.SetActive(false);

        if (!throwButton.gameObject.activeSelf)
            throwButton.gameObject.SetActive(true);

        hitBallsMessage.text = "Great! Now let’s test your precision, aim to the marker";
        tryAgainMessage.text = "Drag the marker here!";

        if (!aimImage.activeSelf)
            aimImage.SetActive(true);

        if (!targetImage.activeSelf)
            targetImage.SetActive(true);
    }

    public void SwitchToSlider()
    {
        if (aimImage.activeSelf)
            aimImage.SetActive(false);

        if (targetImage.activeSelf)
            targetImage.SetActive(false);

        if (!powerSlider.activeSelf)
            powerSlider.SetActive(true);

        hitBallsMessage.text = "Good job! Look the left bar avoid the red area and aim to hit mid of the bar for max power, shoot!";
        tryAgainMessage.text = "Measure your power!";
    }

    public void MeasurePower()
    {

        float gap = powerSlider.GetComponent<Slider>().value;

        VersusAI();

        if (gap >= 0.225 && gap < 0.775)
        {
            playerObj.SetBehaviorThrow();

            tryAgainMessage.text = "Great!!!";


        } else
        {
            tryAgainMessage.text = "Not enough power, try again!";
        }

    }

    public void VersusAI()
    {
        GameManager.Instance.SwitchToEnemy();

    }

    private void UpdateBalls()
    {
        for (int i = 0; i < ballsSprites.Length; i++)
        {
            ballsSprites[i].GetComponent<Image>().sprite = emptyBallSprite;
        }
    }

}
