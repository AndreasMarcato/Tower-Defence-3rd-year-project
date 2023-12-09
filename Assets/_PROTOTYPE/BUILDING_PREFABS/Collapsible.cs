using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapsible : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private GameObject _buildingCanvasButton;
    private Animator _animator;
    [SerializeField] private bool isDestroyable = false;
    [SerializeField] private ParticleSystem _buildingDestroyParticle;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _buildingCanvasButton.SetActive(false);
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && isDestroyable)
            _buildingCanvasButton.SetActive(true);

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player" && isDestroyable)
            _buildingCanvasButton.SetActive(false);

    }

    public void DestroyBuilding()
    {
        if (isDestroyable)
        {
            _audioSource.Play();
            _buildingDestroyParticle.transform.position = this.transform.position;
            _buildingDestroyParticle.Play();
            _animator.SetTrigger("Destroy");
            Destroy(_buildingCanvasButton);
            Destroy(this);
        }
    }
    private void Update()
    {
        _buildingCanvasButton.transform.forward = Camera.main.transform.forward;

    }
}
