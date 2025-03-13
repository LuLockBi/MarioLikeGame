using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] private AudioSource _starMusicSource;
    [SerializeField] private AudioSource _backgroundSource;
    [SerializeField] private AudioClip _defaultBackgroundMusic;
    [SerializeField] private AudioClip _victoryMusic;

    [SerializeField][Range(0f, 1f)] private float _masterVolume = 1f;
    private bool _isPaused = false;
    private bool _isBackgroundPlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if(_effectsSource == null)
                _effectsSource = gameObject.AddComponent<AudioSource>();
            if (_starMusicSource == null)
                _starMusicSource = gameObject.AddComponent<AudioSource>();
            if (_backgroundSource == null)
                _backgroundSource = gameObject.AddComponent<AudioSource>();
            _starMusicSource.spatialBlend = 0f;
            _backgroundSource.spatialBlend = 0f;

            _backgroundSource.loop = true;
            PlayBackgroundMusic(_defaultBackgroundMusic, 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        UnsecuredEventBus.OnPauseToggle += HandlePauseToggle;
        UnsecuredEventBus.OnFlagReached += HandleFlagReached;
        UnsecuredEventBus.OnFlagLowered += HandleFlagLowered;
    }
    private void OnDisable()
    {
        UnsecuredEventBus.OnPauseToggle -= HandlePauseToggle;
        UnsecuredEventBus.OnFlagReached -= HandleFlagReached;
        UnsecuredEventBus.OnFlagLowered -= HandleFlagLowered;
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            _effectsSource.PlayOneShot(clip, volume * _masterVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        _masterVolume = Mathf.Clamp01(volume); 
    }

    public void PlayStarMusic(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            _isBackgroundPlaying = false;
            _starMusicSource.Stop();
            _starMusicSource.clip = clip;
            _starMusicSource.volume = volume;
            _starMusicSource.Play();
        }
        
    }
    public void StopStarMusic()
    {
        _starMusicSource.Stop();
        _starMusicSource.clip = null;
        ResumeBackgroundMusic();
    }


    public void PlayBackgroundMusic(AudioClip clip, float volume = 1f)
    {
        if (clip != null && _backgroundSource.clip != clip)
        {
            _isBackgroundPlaying = true;
            _backgroundSource.clip = clip;
            _backgroundSource.volume = volume;
            _backgroundSource.Play();
        }
    }
    public void PauseStarMusic()
    {
        _starMusicSource.Pause();
    }
    public void PauseBackgroundMusic()
    {
        _backgroundSource.Pause();
    }

    public void ResumeBackgroundMusic()
    {
        _isBackgroundPlaying = true;
        _backgroundSource.Play();
    }
    public void ResumeStarMusic()
    {
        _starMusicSource.Play();
    }

    private void HandlePauseToggle()
    {
        if (!_isPaused)
        {
            if (_isBackgroundPlaying)
            {
                PauseBackgroundMusic();
            }
            else
            {
                PauseStarMusic();
            }
            _isPaused = true;
        }
        else
        {
            if (_isBackgroundPlaying)
            {
                ResumeBackgroundMusic();
            }
            else
            {
                ResumeStarMusic();
            }
            _isPaused = false;
        }
    }

    private void HandleFlagReached(Vector3 position, int points)
    {
        _backgroundSource.Stop(); 
        _isBackgroundPlaying = false;
    }

    private void HandleFlagLowered()
    {
        if (_victoryMusic != null)
        {
            _backgroundSource.loop = false; 
            _backgroundSource.clip = _victoryMusic;
            _backgroundSource.volume = 0.5f;
            _backgroundSource.Play();
        }
    }
}
