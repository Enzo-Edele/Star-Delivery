using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    float scrollX;
    float scrollY;
    Renderer rendererBelt;
    [SerializeField]ConvoyerBelt belt;

    void Awake()
    {
        rendererBelt = GetComponent<Renderer>();
        rendererBelt.material = new Material(rendererBelt.material);
        scrollY = belt.speed / 2;
        Vector2 scale = new Vector2(transform.localScale.x / 2, transform.localScale.z / 2);
        rendererBelt.material.mainTextureScale = scale;
    }
    void Update()
    {
        if (belt.isOn) {
            float offsetX = Time.time * scrollX;
            float offsetY = Time.time * scrollY;
            rendererBelt.sharedMaterial.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
    }
}
