using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitSlider : MonoBehaviour
{
    private float progress;
    private Slider hitSliderProgress;
    private float speed = 0.3f;

    private bool isGoingDown;

    // Start is called before the first frame update
    void Awake()
    {
        hitSliderProgress = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGoingDown)
        {
            hitSliderProgress.value += speed * Time.deltaTime;

            if (hitSliderProgress.value > 0.95f)
                isGoingDown = true;
        } else
        {
            hitSliderProgress.value -= speed * Time.deltaTime;

            if (hitSliderProgress.value < 0.05f)
                isGoingDown = false;
        }
    }
}
