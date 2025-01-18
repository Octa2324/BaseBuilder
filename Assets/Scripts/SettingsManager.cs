using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public CanvasGroup imageCanvasGroup; 
    public float fadeDuration = 1f; 

    private bool isImageVisible = false;

    private void Start()
    {
        if (imageCanvasGroup != null)
        {
            imageCanvasGroup.alpha = 0; 
        }
    }

    public void ToggleImage()
    {
        if (imageCanvasGroup != null)
        {
            isImageVisible = !isImageVisible;

            if (isImageVisible)
            {
                StartCoroutine(FadeCanvasGroup(imageCanvasGroup, 0f, 1f, fadeDuration)); 
            }
            else
            {
                StartCoroutine(FadeCanvasGroup(imageCanvasGroup, 1f, 0f, fadeDuration)); 
            }
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
