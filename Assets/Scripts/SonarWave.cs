using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarWave : MonoBehaviour
{
    public float expandSpeed = 10f;
    public float maxScale = 25f;
    public float lifeTime = 2f;

    private float timer = 0f;
    private Renderer rend;
    private Color startColor;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null) startColor = rend.material.color;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifeTime;

        float scale = Mathf.Lerp(0.1f, maxScale, t);
        transform.localScale = Vector3.one * scale;

        if (rend != null)
        {
            Color c = startColor;
            c.a = 1f - t;
            rend.material.color = c;
        }

        if (timer >= lifeTime)
            Destroy(gameObject);
    }
}

