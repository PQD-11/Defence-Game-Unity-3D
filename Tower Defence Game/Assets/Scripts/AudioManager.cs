using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    [SerializeField] private AudioSource menuMusic, levelSelectMusic;
    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private AudioSource[] sfx;

    private int currentBGM;
    private bool isPlayingBGM;

    private void Update()
    {
        if (isPlayingBGM)
        {
            if (!bgm[currentBGM].isPlaying)
            {
                currentBGM = ++currentBGM % bgm.Length;
                bgm[currentBGM].Play();
            }
        }
    }

    public void StopMusic()
    {
        menuMusic.Stop();
        levelSelectMusic.Stop();
        bgm[currentBGM].Stop();
        isPlayingBGM = false;
    }

    public void PlayMenuMusic()
    {
        StopMusic();
        menuMusic.Play();
    }

    public void PlayLevelSelectMusic()
    {
        StopMusic();
        levelSelectMusic.Play();
    }

    public void PlayBGM()
    {
        StopMusic();

        currentBGM = UnityEngine.Random.Range(0, bgm.Length);
        bgm[currentBGM].Play();
        isPlayingBGM = true;
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
}
