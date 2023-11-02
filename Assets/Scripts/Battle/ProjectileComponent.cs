using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileComponent : MonoBehaviour
{
    [SerializeField] private float timeToLive;
    [SerializeField] private float speed;

    private float countdown;
    private Rigidbody myRigidbody;

    public float Damage;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Setup(Vector3 position, Quaternion rotation, float damage)
    {
        transform.position = position;
        transform.rotation = rotation;
        Damage = damage;

        countdown = timeToLive;
        myRigidbody.velocity = transform.rotation * Vector3.forward * speed;
    }
   
}
