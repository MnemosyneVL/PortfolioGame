using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorAnimationScript : MonoBehaviour
{
    public SpriteRenderer beamerOn;
    public ParticleSystem beamerEffect;
    public float lerpTime = 0.3f;

    private void Start()
    {
        beamerOn.color = new Color(beamerOn.color.r, beamerOn.color.g, beamerOn.color.b, 0f);
    }

    public void OnStart()
    {
        beamerEffect.Play();
        StartCoroutine(AppearAnimation(beamerOn, beamerOn.color.a, 1, lerpTime));
    }
    public void OnEnd()
    {
        beamerEffect.Stop();
        StartCoroutine(AppearAnimation(beamerOn, beamerOn.color.a, 0f, lerpTime));
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
