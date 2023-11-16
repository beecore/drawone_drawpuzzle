using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;
    private Vector2 savedOffset;

    private void Start()
    {
        savedOffset = GetComponent<Renderer>().material.GetTextureOffset("_MainTex");
    }

    private void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, savedOffset.y);
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
    }
}