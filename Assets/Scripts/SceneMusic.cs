using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip musicaCena;

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.TocarMusica(musicaCena);
        }
    }
}
