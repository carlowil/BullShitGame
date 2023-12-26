using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothImageColor : MonoBehaviour
{
    public Color from;
    public Color to;

    public float duration = 1f;
    private Image _image;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        var startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            _image.color = Color.Lerp(from, to, (Time.time - startTime) / duration);
            yield return 0;
        }
        
        _image.color = to;
    }
}