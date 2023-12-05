using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    [SerializeField] private AudioSource[] _audioSource;
    float _elapsedTime;
    public void PlayerMoveAudio()
    {
        if (_elapsedTime > 0)
            return;

        int audioIndex;
        audioIndex = Random.Range(0, _audioSource.Length);
        switch (audioIndex)
        {
            case 0:
                _audioSource[audioIndex].Play();
                break;
            case 1:
                _audioSource[audioIndex].Play();
                break;
            case 2:
                _audioSource[audioIndex].Play();
                break;
            default:
                return;
        }
        _elapsedTime = 10;
    }

    private void Update()
    {
        if (_elapsedTime > 0)
        {
            _elapsedTime -= Time.deltaTime;
        }
    }
}
