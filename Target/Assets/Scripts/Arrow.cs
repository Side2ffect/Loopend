using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody arrowRigidbody;
    private void Awake()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        float speed = 20f;
        arrowRigidbody.velocity = transform.forward*speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
