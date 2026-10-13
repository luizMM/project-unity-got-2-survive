using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Música")]
    [SerializeField] private AudioSource musicSource;

    [Header("Volume")]
    [SerializeField, Range(0f, 1f)]
    private float volumeMusica = 0.5f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = volumeMusica;
    }

    public void TocarMusica(AudioClip musica)
    {
        if (musica == null)
            return;

        // Evita reiniciar a mesma música
        if (musicSource.clip == musica && musicSource.isPlaying)
            return;

        musicSource.clip = musica;
        musicSource.Play();
    }

    public void PararMusica()
    {
        musicSource.Stop();
    }

    public void AjustarVolume(float volume)
    {
        volumeMusica = Mathf.Clamp01(volume);
        musicSource.volume = volumeMusica;
    }
}
