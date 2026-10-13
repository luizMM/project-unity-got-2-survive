using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip somClique;

    public void TocarClique()
    {
        if (audioSource != null && somClique != null)
            audioSource.PlayOneShot(somClique);
    }
}
