using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text text;

    public Image Image
    {
        get
        {
            if (image) return image;

            image = GetComponent<Image>();

            return image;
        }
    }

    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform) return rectTransform;

            rectTransform = GetComponent<RectTransform>();

            return rectTransform;
        }
    }

    public TMP_Text Text
    {
        get
        {
            if (text) return text;

            text = GetComponentInChildren<TMP_Text>();

            return text;
        }
    }
}