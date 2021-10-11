using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasButtons : MonoBehaviour
{
    public Sprite button, buttonPressed;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetString("First Game") == "No")
            StartCoroutine(LoadScene("Game"));
        else
        {
            StartCoroutine(LoadScene("Study"));
        }
    }

    public void RestartGame()
    {
        StartCoroutine(LoadScene("Game"));
    }

    public void SetPressedButton()
    {
        image.sprite = buttonPressed;
        transform.GetChild(0).localPosition -= new Vector3(0, 5, 0);
    }

    public void SetDefaultButton()
    {
        image.sprite = button;
        transform.GetChild(0).localPosition += new Vector3(0, 5, 0);
    }

    IEnumerator LoadScene(string nameScene)
    {
        float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(nameScene);
    }
}
