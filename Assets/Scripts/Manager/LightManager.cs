using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : SingleBase<LightManager>
{
    [SerializeField] private Light2D mainLight;

    private void Start()
    {
        GameManager.Instance.OnBattleStart += Instance_OnBattleStart;
        GameManager.Instance.OnPrepareStart += Instance_OnPrepareStart;
    }

    private void Instance_OnPrepareStart(object sender, System.EventArgs e)
    {
        Debug.Log("亮");
        StartCoroutine(ChangeToDay());
    }

    private void Instance_OnBattleStart(object sender, System.EventArgs e)
    {
        Debug.Log("暗");
        StartCoroutine(ChangeToNight());
    }

    private float dayDuration = 1.5f;
    private float nightDuration = 1.5f;
    public Color nightColor;
    public IEnumerator ChangeToDay()
    {
        //float elapsedTime = 0f;
        //float startIntensity = mainLight.intensity;
        //float targetIntensity = 1f;

        //while (elapsedTime < 1f)
        //{
        //    elapsedTime += Time.deltaTime / dayDuration;
        //    mainLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime);
        //    yield return null;
        //}

        float elapsedTime = 0f;
        Color currentColor = mainLight.color;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime / dayDuration;
            mainLight.color = Color.Lerp(currentColor, Color.white, elapsedTime);
            yield return null;
        }
    }

    private IEnumerator ChangeToNight()
    {
        //float elapsedTime = 0f;
        //float startIntensity = mainLight.intensity;
        //float targetIntensity = 0.2f;

        //while (elapsedTime < 1f)
        //{
        //    Debug.Log(elapsedTime);
        //    elapsedTime += Time.deltaTime / nightDuration;
        //    mainLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime);
        //    yield return null;
        //}

        float elapsedTime = 0f;
        Color currentColor = mainLight.color;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime / dayDuration;
            mainLight.color = Color.Lerp(currentColor, nightColor, elapsedTime);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnBattleStart -= Instance_OnBattleStart;
        GameManager.Instance.OnPrepareStart -= Instance_OnPrepareStart;
    }
}
