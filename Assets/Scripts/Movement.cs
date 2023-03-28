using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1100f;
    [SerializeField] float rotateCoef = 75f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(rotateCoef);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-rotateCoef);
        }
    }

    private void Rotate(float coefficient)
    {
        rb.freezeRotation = true;
        transform.Rotate(coefficient * Vector3.forward * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
