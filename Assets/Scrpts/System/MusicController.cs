using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {
    
    private enum FadeState
    {
        Low,
        FadeAway,
        FadeIn,
        High
    }

    #region public Member
    /// <summary>
    /// 音量大小
    /// </summary>
    public float volume { get; private set; } = 0.75f;

    public bool isMute { get; private set; } = false;

    [Tooltip("所有音乐播放器")]
    public GameObject[] AllMusicAudios;
    [Tooltip("所有音效播放器")]
    public GameObject[] AllSoundEffectsAudios;
    /// <summary>
    /// 所有音乐播放器
    /// </summary>
    [HideInInspector]
    public List<AudioSource> AllMusicAudioSource;
    /// <summary>
    /// 所有音效播放器
    /// </summary>
    [HideInInspector]
    public List<AudioSource> AllSoundEffectsAudioSource;
    #endregion

    #region 单例实现
    public static MusicController Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			MusicController instance = FindObjectOfType<MusicController>();

			if(instance!=null)
			{
				return instance;
			}
			return m_Instance;
		}
	}
    #endregion

    #region private Member
    // 唯一实例
    private static MusicController m_Instance;
    private float LastVolume;
    private Coroutine FadeAwayCoroutine;
    private Coroutine FadeInCoroutine;
    private FadeState fadeState;
    #endregion

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
        InitMusicAudios();
        InitSoundEffectsAudios();
        fadeState = FadeState.High;
    }

    #region public Method
    /// <summary>
    /// 禁用所有音乐
    /// </summary>
    public void StopAllMusic()
    {
        isMute = true;
        foreach(var audio in AllMusicAudioSource)
        {
            audio.mute = true;
        }
    }
    /// <summary>
    /// 禁用所有音效
    /// </summary>
    public void StopAllSoundEffects()
    {
        isMute = true;
        foreach (var audio in AllSoundEffectsAudioSource)
        {
            audio.mute = true;
        }
    }
    /// <summary>
    /// 启用所有音乐
    /// </summary>
    public void OpenAllMusic()
    {
        isMute = false;
        foreach (var audio in AllMusicAudioSource)
        {
            audio.mute = false;
        }
    }
    /// <summary>
    /// 启用所有音效
    /// </summary>
    public void OpenAllSoundEffects()
    {
        isMute = false;
        foreach (var audio in AllSoundEffectsAudioSource)
        {
            audio.mute = false;
        }
    }

    public void StartToFadeAway()
    {
        FadeAwayCoroutine = StartCoroutine(FadeAway());
    }
    public void StopFadeAway()
    {
        StopCoroutine(FadeAwayCoroutine);
        fadeState = FadeState.Low;
    }

    public void StartToFadeIn()
    {
        FadeInCoroutine = StartCoroutine(FadeIn());
    }
    public void StopFadeIn()
    {
        StopCoroutine(FadeInCoroutine);
        fadeState = FadeState.High;
    }
    /// <summary>
    /// 调整所有音量大小
    /// </summary>
    /// <param name="size"></param>
    public void SetAllSound(float size)
    {
        volume = size;
        foreach (var item in AllMusicAudios)
        {
            item.GetComponent<AudioSource>().volume = size;
        }
    }
    #endregion

    #region private Method
    // 初始化所有音乐
    private void InitMusicAudios()
    {
        AllMusicAudioSource = new List<AudioSource>();
        foreach (var go in AllMusicAudios)
        {
            AudioSource audioSource;
            DontDestroyOnLoad(go);
            audioSource = go.GetComponent<AudioSource>();
            audioSource.volume = volume;
            AllMusicAudioSource.Add(audioSource);
        }
    }
    // 初始化所有音效
    private void InitSoundEffectsAudios()
    {
        AllSoundEffectsAudioSource = new List<AudioSource>();
        foreach (var go in AllSoundEffectsAudios)
        {
            AudioSource audioSource;
            DontDestroyOnLoad(go);
            audioSource = go.GetComponent<AudioSource>();
            audioSource.volume = volume;
            AllSoundEffectsAudioSource.Add(audioSource);
        }
    }
    private IEnumerator FadeAway()
    {
        fadeState = FadeState.FadeAway;
        LastVolume = AllMusicAudioSource[0].volume;
        while (true)
        {
            if (AllMusicAudioSource[0].volume <= 0)
            {
                foreach (var audio in AllMusicAudioSource)
                {
                    audio.volume = 0;
                }
                break;
            }
            foreach (var audio in AllMusicAudioSource)
            {
                audio.volume -= 0.02f;
            }
            yield return 0;
        }
        fadeState = FadeState.Low;
    }
    private IEnumerator FadeIn()
    {
        fadeState = FadeState.FadeIn;
        while (true)
        {
            if (AllMusicAudioSource[0].volume >= LastVolume)
            {
                foreach (var audio in AllMusicAudioSource)
                {
                    audio.volume = LastVolume;
                }
                break;
            }
            foreach (var audio in AllMusicAudioSource)
            {
                audio.volume += 0.02f;
            }
            yield return 0;
        }
        fadeState = FadeState.High;
    }
    #endregion

}