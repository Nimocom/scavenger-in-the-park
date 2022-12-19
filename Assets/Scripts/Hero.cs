using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class Hero : MonoBehaviour
{
    public static Hero inst;

    [SerializeField] MovingPlatform platform;

    [SerializeField] Transform handPoint;

    [SerializeField] float movementSmoothness;

    Animator animator;
    Vector3 lerpedVector;

    Garbage currentGarbage;
    Transform cameraTransform;

    List<Garbage> garbage;

    [SerializeField] Rig rightArmRig;
    [SerializeField] Rig leftHandRig;

    private void Awake()
    {
        inst = this;

        animator = GetComponent<Animator>();

        garbage = new List<Garbage>();

    }

    void Start()
    {
        cameraTransform = CameraController.inst.transform;
    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        if (Input.GetKey(KeyCode.LeftShift))
        {
            platform.ApplyMovement(x, z);
            lerpedVector = Vector3.Lerp(lerpedVector, Vector3.zero, movementSmoothness * Time.deltaTime);
            leftHandRig.weight = Mathf.Lerp(leftHandRig.weight, 1f, 12f * Time.deltaTime);
        }
        else
        {
            lerpedVector = Vector3.Lerp(lerpedVector, new Vector3(x, 0f, z), movementSmoothness * Time.deltaTime);
            leftHandRig.weight = Mathf.Lerp(leftHandRig.weight, 0f, 12f * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
            platform.ApplyMovement(0f, 0f);

        animator.SetFloat("XAxis", lerpedVector.x);
        animator.SetFloat("ZAxis", lerpedVector.z);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lifting") || animator.IsInTransition(0))
            return;

        if (x != 0 || z != 0f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f), 12f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E) && currentGarbage == null && CheckArray())
            animator.SetTrigger("PickUp");

        if (currentGarbage != null)
        {
            if (Input.GetMouseButton(1))
                rightArmRig.weight = Mathf.Lerp(rightArmRig.weight, 1f, 8f * Time.deltaTime);
            else
                rightArmRig.weight = Mathf.Lerp(rightArmRig.weight, 0f, 6f * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
                DropTheGarbage();
        }
        else
            rightArmRig.weight = Mathf.Lerp(rightArmRig.weight, 0f, 8f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Garbage"))
            garbage.Add(other.GetComponent<Garbage>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Garbage"))
            garbage.Remove(other.GetComponent<Garbage>());
    }

    public void TakeTheGarbage()
    {
        for (int i = 0; i < garbage.Count; i++)
            if (garbage[i] != null)
            {
                if (garbage[i].rigidbody.isKinematic)
                {
                    garbage.RemoveAt(i);
                    continue;
                }

                currentGarbage = garbage[i];

                currentGarbage.transform.SetPositionAndRotation(handPoint.position, handPoint.rotation);
                currentGarbage.transform.SetParent(handPoint);

                currentGarbage.rigidbody.isKinematic = true;

                garbage.RemoveAt(i);

                UIManager.inst.ShowInfo(currentGarbage.name, currentGarbage.description);

                return;
            }
    }

    bool CheckArray()
    {
        for (int i = 0; i < garbage.Count; i++)
            if (garbage[i] != null)
            {
                if (garbage[i].rigidbody.isKinematic)
                    garbage.RemoveAt(i);
            }

        return garbage.Count > 0;
    }

    public void DropTheGarbage()
    {
        currentGarbage.transform.SetParent(null);
        currentGarbage.rigidbody.isKinematic = false;

        currentGarbage = null;
    }
}
