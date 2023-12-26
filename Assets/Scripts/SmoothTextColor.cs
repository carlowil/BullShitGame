using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmoothTextColor : MonoBehaviour
{
    public Color from;
    public Color to;

    public float duration = 1f;
    private TMP_Text _text;


    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
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
            _text.color = Color.Lerp(from, to, (Time.time - startTime) / duration);
            yield return 0;
        }
        
        _text.color = to;
    }
}