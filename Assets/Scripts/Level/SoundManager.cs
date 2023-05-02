using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _loopSource, _singleSource;

    internal static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        _singleSource.PlayOneShot(clip);
    }

    public void ChangeSoundtrack(AudioClip soundtrack)
    {
        _loopSource.clip = soundtrack;
        _loopSource.Play();
    }
}
