using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    public float scrollX = 0f;
    public float scrollY = 0.5f;
    Renderer renderer;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = new Material(renderer.material);
    }
    void Update()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        renderer.sharedMaterial.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
