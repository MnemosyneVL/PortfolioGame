using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAnimationScript : MonoBehaviour
{
    public SpriteRenderer keyboard;
    public SpriteRenderer screen;
    public ParticleSystem beamer;
    public float lerpTime;

    private void Start()
    {
        keyboard.color = new Color(keyboard.color.r, keyboard.color.g, keyboard.color.b, 0f);
        screen.color = new Color(screen.color.r, screen.color.g, screen.color.b, 0.5f);
    }

    public void OnStart()
    {
        beamer.Play();
        StartCoroutine(AppearAnimation(keyboard, keyboard.color.a, 1, lerpTime));
        StartCoroutine(AppearAnimation(screen, screen.color.a, 1, lerpTime));
    }
    public void OnEnd()
    {
        beamer.Stop();
        StartCoroutine(AppearAnimation(keyboard, keyboard.color.a, 0f, lerpTime));
        StartCoroutine(AppearAnimation(screen, screen.color.a, 0.5f, lerpTime));
    }

    private IEnumerator AppearAnimation(SpriteRenderer renderer, float start, float end, float lerpTime)
    {

        float startTime = Time.time;
        float workTime = 0f;
        float finalPosition = 0f;
        while (true)
        {
            workTime = Time.time - startTime;
            finalPosition = workTime / lerpTime;
            float currentValue = Mathf.Lerp(start, end, finalPosition);

            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, currentValue);


            if (finalPosition >= 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
