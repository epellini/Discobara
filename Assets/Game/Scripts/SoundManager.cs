using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public List<AudioClip> musicTracks;
    [SerializeField] private AudioSource _musicSource, _effectsSource;
    [SerializeField] public AudioClip[] audioClips;
    [SerializeField] private AudioClip menuMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMenuMusic()
    {
        if (menuMusic != null)
        {
            _musicSource.clip = menuMusic;
            _musicSource.loop = true; // Loop the menu music
            _musicSource.Play();
        }
    }

    public void StopMenuMusic()
    {
        _musicSource.Stop();
    }

    public void StopGameMusic()
    {
        _musicSource.Stop();
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayDeathSound());
    }

    public bool IsMenuMusicPlaying()
    {
        return _musicSource.isPlaying && _musicSource.clip == menuMusic;
    }

    public bool IsGameMusicPlaying()
    {
        return _musicSource.isPlaying && _musicSource.clip != menuMusic;
    }
    private IEnumerator PlayDeathSound()
    {
        float duration = 2f; // Duration of the effect in seconds
        float startPitch = _musicSource.pitch;
        float endPitch = 0.5f; // Lower pitch to create 'melting' sound
        float startVolume = _musicSource.volume;
        float endVolume = 0f; // Target volume is 0 for fade-out
        float time = 0;

        while (time < duration)
        {
            _musicSource.pitch = Mathf.Lerp(startPitch, endPitch, time / duration);
            _musicSource.volume = Mathf.Lerp(startVolume, endVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        _musicSource.Stop();
        _musicSource.pitch = startPitch;
        _musicSource.volume = startVolume;
    }

    public void Start()
    {
        //PlayRandomSong();
    }

    public void RestartMusic()
    {
        StopAllCoroutines();
        _musicSource.Stop(); // Stop current music
        _musicSource.pitch = 1f; // Reset pitch
        _musicSource.volume = 1f; // Reset volume
        PlayRandomSong(); // Play a new random song
    }

    public void PlayRandomSong()
    {
        if (musicTracks.Count > 0)
        {
            _musicSource.clip = musicTracks[Random.Range(0, musicTracks.Count)];
            _musicSource.Play();
            _musicSource.loop = true; // Loop the menu music
        }
    }

    public void ButtonPress()
    {
        _effectsSource.PlayOneShot(audioClips[0]);
    }
    private int pitchCounter = 0;

    public void RightMove()
    {
        // Define an array of pitches
        float[] pitches = new float[] {  1.3f, 1.4f, 1.5f, 1.6f, 1.4f, 1.5f };

        // Set the pitch based on the current counter
        _effectsSource.pitch = pitches[pitchCounter];

        // Play the sound effect
        _effectsSource.PlayOneShot(audioClips[1]);
        // Increment the counter, and reset if it reaches the length of the pitches array
        pitchCounter = (pitchCounter + 1) % pitches.Length;
    }

    public void WrongMove()
    {
        _effectsSource.PlayOneShot(audioClips[2]);
    }

        public void NewHighScore()
    {
        _effectsSource.PlayOneShot(audioClips[4]);
    }

    public void Bite()
    {
        _effectsSource.PlayOneShot(audioClips[3]);
    }

}
