using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class LevelSelect_Button : MonoBehaviour
{
    private Image[] _fadeImages;
    private TextMeshProUGUI[] _fadeTexts;

    internal virtual void Awake()
    {
        _fadeImages = GetComponentsInChildren<Image>();
        _fadeTexts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void ChangeButtonFade(string state)
    {
        if (state == "fade")
        {
            foreach (Image image in _fadeImages)
            {
                image.color = ChangeColor(image.color, 0.3f);
            }
            foreach (TextMeshProUGUI text in _fadeTexts)
            {
                text.color = ChangeColor(text.color, 0.3f);
            }
        }
        else if (state == "unfade")
        {
            foreach (Image image in _fadeImages)
            {
                image.color = ChangeColor(image.color, 1f);
            }
            foreach (TextMeshProUGUI text in _fadeTexts)
            {
                text.color = ChangeColor(text.color, 1f);
            }
        }
    }

    private Color ChangeColor(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
