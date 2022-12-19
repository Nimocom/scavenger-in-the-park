using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinTrigger : MonoBehaviour
{
    [SerializeField] Garbage.GarbageType garbageType;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Garbage"))
        {
            other.GetComponent<Garbage>().rigidbody.isKinematic = true;
            other.GetComponent<Collider>().enabled = false;

            other.transform.SetParent(transform.root);

            UIManager.inst.ShowResult(other.GetComponent<Garbage>().garbageType == garbageType);

        }
    }
}
