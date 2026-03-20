using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SESpeaker : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void Play(AudioClip clip)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(Checking(() =>
        {
            Destroy(this.gameObject);
        }));
    }

    public delegate void functionType();
    private IEnumerator Checking(functionType callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audioSource.isPlaying)
            {
                callback();
                break;
            }
        }
    }
}
