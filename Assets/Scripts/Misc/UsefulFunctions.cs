using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UsefulFunctions : MonoBehaviour
{
    private static float fadingAlpha = 0f;
    private static bool fadingInterrupted = false;
    /// <summary>
    /// Fade the screen to or from black.<br/>
    /// If the screen is already fading FROM black, and TO black is called, then interrupt FROM. Continue TO from the current alpha level.
    /// </summary>
    /// <param name="intent">'to' = Fade TO black<br/>'from' = Fade FROM black</param>
    /// <param name="fadeScreenOverlay">The fading image component that covers the screen</param>
    /// <param name="secondsToFade">Approximate time it takes for screen to fade in or out. Default is 0.4f</param>
    public static IEnumerator FadeScreenBlack(string intent, Image fadeScreenOverlay, float secondsToFade = 0.4f)
    {
        Color origColor = fadeScreenOverlay.color;
        fadeScreenOverlay.gameObject.SetActive(true);
        if (intent == "from") // Fade FROM black
        {
            fadingAlpha = 1f;
            while (fadingAlpha > 0f && !fadingInterrupted)
            {
                fadingAlpha -= 0.04f;
                fadeScreenOverlay.color = new Color(origColor.r, origColor.g, origColor.b, fadingAlpha);
                yield return new WaitForSeconds(secondsToFade / 25f);
            }
            if (!fadingInterrupted)
                fadeScreenOverlay.gameObject.SetActive(false);
        }
        else if (intent == "to") // Fade TO black
        {
            if (fadeScreenOverlay.color.a != 0f)
                fadingInterrupted = true;

            while (fadingAlpha < 1f)
            {
                fadingAlpha += 0.04f;
                fadeScreenOverlay.color = new Color(origColor.r, origColor.g, origColor.b, fadingAlpha);
                yield return new WaitForSeconds(secondsToFade / 25f);
            }
            fadingInterrupted = false;
        }
    }

    /// <summary>
    /// Fade a group of objects. Only supports fading out.<br/>
    /// </summary>
    /// <param name="sprites">Which Renderer objects to fade.</param>
    /// <param name="secondsToFade">Time to fade.</param>
    public static IEnumerator FadeRenderers(SpriteRenderer[] sprites, float secondsToFade)
    {
        for (int fadeTick = 100; fadeTick >= 0f; fadeTick -= 5)
        {
            foreach (SpriteRenderer spr in sprites)
            {
                Color origColor = spr.color;
                spr.color = new Color(origColor.r, origColor.g, origColor.b, fadeTick/100f);
            }
            yield return new WaitForSeconds(secondsToFade / 20f);
        }
    }
}
