using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OxygenSystem : MonoBehaviour
{
    public float maxOxygen = 100f;
    public float drainPerSecond = 8f;
    public float refillPerSecond = 20f;

    public Slider oxygenSlider;
    public Transform waterSurface;

    private float currentOxygen;

    void Start()
    {
        currentOxygen = maxOxygen;

        if (oxygenSlider != null)
        {
            oxygenSlider.maxValue = maxOxygen;
            oxygenSlider.value = currentOxygen;
        }
    }

    void Update()
    {
        bool isUnderwater = transform.position.y < waterSurface.position.y;

        if (isUnderwater)
            currentOxygen -= drainPerSecond * Time.deltaTime;
        else
            currentOxygen += refillPerSecond * Time.deltaTime;

        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);

        if (oxygenSlider != null)
            oxygenSlider.value = currentOxygen;

        if (currentOxygen <= 0f && GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDied();
        }
    }

    public void Refill(float amount)
    {
        currentOxygen = Mathf.Min(maxOxygen, currentOxygen + amount);
    }
}
