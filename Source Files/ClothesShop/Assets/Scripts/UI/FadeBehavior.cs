using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeBehavior : MonoBehaviour
{

    private CanvasGroup cg;
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        cg = GetComponent<CanvasGroup>();
        text = GetComponent<TextMeshProUGUI>();

        Interaction.isInteractable.AddListener(Fade);
    }

    private void Fade(bool fadeIn, string prompt)
    {
        text.text = prompt;
        StartCoroutine(FadeController(fadeIn));
    }

    IEnumerator FadeController(bool fadeIn)
    {
        if (!fadeIn)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                cg.alpha = i;
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                cg.alpha = i;
                yield return null;
            }

        }
    }
}
