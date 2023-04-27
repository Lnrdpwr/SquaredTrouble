using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _loopSource, _singleSource;

    internal static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameObject[] allManagers = GameObject.FindGameObjectsWithTag("SoundManager");
        if(allManagers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
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
