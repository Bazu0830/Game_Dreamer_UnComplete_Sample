using UnityEngine;
using UnityEngine.Audio;

public class WalkSE : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private AudioMixerGroup audioMixerGroup;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = CreateAudioSource();
    }

    public void WalkSound(string eventName)
    {
        audioSource.Play();
    }

    private AudioSource CreateAudioSource()
    {
        var audioGameObject = new GameObject();
        audioGameObject.name = "WalkSE";
        audioGameObject.transform.SetParent(gameObject.transform);

        var audioSource = audioGameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = audioMixerGroup;

        return audioSource;
    }
}