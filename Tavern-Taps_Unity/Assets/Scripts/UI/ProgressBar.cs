using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image ProgressBarFill;

    private float max;

    public void setupProgressBar(float _max)
    {
        max = _max;
    }

    public void updateProgressBar(float track)
    {
        Vector2 sizeVector = new Vector3(track / max, 1, 1);
        ProgressBarFill.rectTransform.localScale = sizeVector;
    }
}
