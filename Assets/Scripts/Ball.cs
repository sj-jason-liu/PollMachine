using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField]
    private float _speed = 1.5f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Speedup")
        {
            Vector3 direction = transform.position - other.transform.position;
            _rb.AddForceAtPosition(direction.normalized * _speed, transform.position);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ball")
        {
            Vector3 direction = transform.position - other.transform.position;
            _rb.AddForceAtPosition(direction.normalized * _speed, transform.position);
        }
    }
}
