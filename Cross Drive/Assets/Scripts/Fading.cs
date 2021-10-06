using UnityEngine;

public class Fading : MonoBehaviour
{
    public Texture2D fading;

    private float fadeSpeed = 1f, drawDepth = -1000, alpha = 1f, fadeDir = -1f;

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = (int)drawDepth;
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), fading);

    }

    public float Fade(float direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }
}
