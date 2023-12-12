using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private Transform _target;
    public GameObject _hitParticleEffect;
    private float travelSpeed = 50f;
    public void ProjectileTarget(Transform target) => _target = target;

    Rigidbody rb;
    [SerializeField] float force;
    [SerializeField] float damage = 20;
    [SerializeField] private bool isPB = true;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
         rb = gameObject.GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        if(_target == null)
        {
            Destroy(gameObject); return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = travelSpeed * Time.deltaTime;

        
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }
    
    

    private void FixedUpdate()
    {
        //rb.AddRelativeForce(Vector3.forward * force, ForceMode.Acceleration);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isPB)
            return;
        if (collision.gameObject.tag == "Enemy" && !isPB)
            return;

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            GameObject effectInstance = Instantiate(_hitParticleEffect, transform.position, transform.rotation);
            Destroy(effectInstance, 2f);
            Destroy(gameObject);
            return;
        }
        else
            Destroy(gameObject);
    }
}
