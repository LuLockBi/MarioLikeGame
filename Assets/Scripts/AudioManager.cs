using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource _audioSource;

    [SerializeField][Range(0f, 1f)] private float _masterVolume = 1f; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip, volume * _masterVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        _masterVolume = Mathf.Clamp01(volume); 
    }
}
