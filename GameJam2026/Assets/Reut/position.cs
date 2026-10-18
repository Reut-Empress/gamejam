using UnityEngine;
using UnityEngine.InputSystem;

public class position : MonoBehaviour
{
    [SerializeField] private float dotSize = 20f;
    [SerializeField] private Color dotColor = Color.yellow;

    private Texture2D dotTexture;

    private void Awake()
    {
        dotTexture = CreateCircleTexture(64);
    }


    private void OnGUI()
    {
        Pointer pointer = Pen.current != null ? Pen.current : Pointer.current;
        if (pointer == null) return;

        Vector2 pos = pointer.position.ReadValue();

        Color old = GUI.color;
        GUI.color = dotColor;
        GUI.DrawTexture(
            new Rect(pos.x - dotSize * 0.5f,
                     Screen.height - pos.y - dotSize * 0.5f,
                     dotSize, dotSize),
            dotTexture);
        GUI.color = old;
    }

    private static Texture2D CreateCircleTexture(int size)
    {
        var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        float r = size * 0.5f;
        var center = new Vector2(r, r);

        for (int y = 0; y < size; y++)
            for (int x = 0; x < size; x++)
                tex.SetPixel(x, y,
                    Vector2.Distance(new Vector2(x + 0.5f, y + 0.5f), center) <= r
                        ? Color.white : Color.clear);

        tex.Apply();
        return tex;
    }

}
