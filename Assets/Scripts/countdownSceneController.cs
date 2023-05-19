using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownSceneController : MonoBehaviour
{
    public Image countdownImage;
    public Sprite[] numberSprites;
    public float countdownDelay = 4f;
    public float countdownDuration = 1f;
    public string gameSceneName;
    public AudioClip audioClip;

    private void Start()
    {
        countdownImage.enabled = false;
        StartCoroutine(StartCountdownAfterDelay());
    }

    private IEnumerator StartCountdownAfterDelay()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.Play();

        yield return new WaitForSeconds(countdownDelay);

        countdownImage.enabled = true;
        StartCoroutine(StartCountdown());

        yield return new WaitForSeconds(2.5f);

        audioSource.Stop();

    }

    private IEnumerator StartCountdown()
    {
        for (int i = numberSprites.Length - 1; i >= 0; i--)
        {
            countdownImage.sprite = numberSprites[i];
            yield return new WaitForSeconds(countdownDuration);
        }

        SceneManager.LoadScene("StandardMap");
    }
}