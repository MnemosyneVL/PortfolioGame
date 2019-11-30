using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownAnimationScript : MonoBehaviour
{
    public SpriteRenderer beamerOn;
    public SpriteRenderer holoOn;
    public SpriteRenderer holoUp;
    public SpriteRenderer holoDwn;
    public ParticleSystem beamerEffect;
    public float lerpTime = 0.3f;

    private void Start()
    {
        beamerOn.color = new Color(beamerOn.color.r, beamerOn.color.g, beamerOn.color.b, 0f);
        holoOn.color = new Color(holoOn.color.r, holoOn.color.g, holoOn.color.b, 0f);
        holoUp.color = new Color(holoUp.color.r, holoUp.color.g, holoUp.color.b, 0f);
        holoDwn.color = new Color(holoOn.color.r, holoOn.color.g, holoOn.color.b, 0f);
    }

    public void OnStart()
    {
        beamerEffect.Play();
        StartCoroutine(AppearAnimation(beamerOn, beamerOn.color.a, 1, lerpTime));
        StartCoroutine(AppearAnimation(holoOn, holoOn.color.a, 1, lerpTime));
    }
    public void OnEnd()
    {
        beamerEffect.Stop();
        StartCoroutine(AppearAnimation(beamerOn, beamerOn.color.a, 0f, lerpTime));
        StartCoroutine(AppearAnimation(holoOn, holoOn.color.a, 0f, lerpTime));
        StartCoroutine(AppearAnimation(holoUp, holoUp.color.a, 0f, lerpTime));
        StartCoroutine(AppearAnimation(holoDwn, holoDwn.color.a, 0f, lerpTime));
    }

    public void OnUp()
    {
        StartCoroutine(ArrowsAnimation(holoUp, holoUp.color.a, lerpTime));
    }

    public void OnDwn()
    {
        StartCoroutine(ArrowsAnimation(holoDwn, holoDwn.color.a, lerpTime));
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
    private IEnumerator ArrowsAnimation(SpriteRenderer renderer, float start, float lerpTime)
    {

        float startTime = Time.time;
        float workTime = 0f;
        float finalPosition = 0f;
        bool appear = true;
        while (true)
        {
            if(appear)
            {
                workTime = Time.time - startTime;
                finalPosition = workTime / lerpTime;
                float currentValue = Mathf.Lerp(start, 1f, finalPosition);

                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, currentValue);
                if (finalPosition >= 1)
                    appear = false;
            }
            else
            {
                workTime = Time.time - startTime;
                finalPosition = workTime / lerpTime;
                float currentValue = Mathf.Lerp(1f, 0f, finalPosition);

                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, currentValue);
                if (finalPosition <= 0)
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
