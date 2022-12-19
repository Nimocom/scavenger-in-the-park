using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public enum GarbageType
    { 
    Metall,
    Plastic,
    Glass,
    Paper
    }

    public new string name;

    [Multiline]
    public string description;

    public new Rigidbody rigidbody;

    public GarbageType garbageType;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
