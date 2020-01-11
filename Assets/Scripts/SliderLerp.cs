using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLerp : MonoBehaviour
{
    private Slider hpbar;
    public float lerpSpeed = 1.0f;
    private float targetValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetValue = 0;
        hpbar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpbar.value != targetValue)
        {
            hpbar.value = Mathf.Lerp(hpbar.value, targetValue, lerpSpeed * Time.deltaTime);
        }
    }

    public IEnumerator LerpTo(float value)
    {
        while (hpbar.value != value)
        {
            hpbar.value = Mathf.Lerp(hpbar.value, value, lerpSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void SetTargetValue(float value)
    {
        targetValue = value;
    }
}


