using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveInfo : MonoBehaviour
{
    public float timeout = 3f;
    public float fadeTime = 1f;
    public TextMeshProUGUI[] textTemplates;

    private CanvasGroup canvasGroup;
    private float fadeTimer;
    private bool fading = false;
    private string fadeDirection;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void FixedUpdate()
    {
        if (fading)
        {
            Fade();
        }
    }

    public void FadeIn()
    {
        fading = true;
        fadeDirection = "in";
    }

    public void FadeOut()
    {
        fading = true;
        fadeDirection = "out";
    }

    private void Fade()
    {
        float alpha;
        if (fadeDirection == "in")
        {
            alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeTime);
        } else
        {
            alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeTime);
        }

        canvasGroup.alpha = alpha;

        if (fadeTimer <= 0)
        {
            fading = false;
            fadeTimer = fadeTime;
        }

        fadeTimer -= Time.deltaTime;
    }

    public void ChangeWave(int number)
    {
        TemplateText(number);
        FadeIn();
        Invoke("FadeOut", timeout + fadeTime);
    }

    private void TemplateText (int waveNumber)
    {
        foreach (TextMeshProUGUI t in textTemplates)
        {
            Debug.Log(t.text);
            t.SetText(
                t.text
                    .Replace("{{waveNumber}}", waveNumber.ToString())
            );
        }
    }
}
