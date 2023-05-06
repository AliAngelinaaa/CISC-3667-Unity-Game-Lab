using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class leveltext : MonoBehaviour
{
    [SerializeField] private float flashDuration = 2f;
    [SerializeField] private float flashSpeed = 0.5f;

    private TextMeshProUGUI levelText;

    private void Start()
    {
        levelText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FlashTitle());
    }

    private IEnumerator FlashTitle()
    {
        Color startColor = levelText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float timer = 0f;
        while (timer < flashDuration)
        {
            levelText.color = Color.Lerp(startColor, endColor, Mathf.PingPong(timer * flashSpeed, 1f));
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
