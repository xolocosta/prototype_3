using UnityEngine;

public class TileAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audio;
    
    private GameObject _player;
    private AudioSource _audioSource;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = _player.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        System.Random r = new System.Random();
        _audioSource.clip = _audio[r.Next(0, _audio.Length)];
        if (!_audioSource.isPlaying) _audioSource.Play();
    }
}
