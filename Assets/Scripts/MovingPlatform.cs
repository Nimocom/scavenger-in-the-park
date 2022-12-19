using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float torquePower;
    [SerializeField] float forcePower;

    float vertical, horizontal;

    new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    public void ApplyMovement(float x, float z)
    {
        horizontal = x;
        vertical = z;   
    }

    void Update()
    {

    }

    public void FixedUpdate()
    {
        rigidbody.AddTorque(transform.forward * horizontal * torquePower);
        rigidbody.AddForce(transform.up * vertical * forcePower);
    }

}
