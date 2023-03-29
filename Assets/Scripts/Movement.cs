using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    // Parameters    
    [SerializeField] float mainThrust = 1100f;
    [SerializeField] float rotateCoef = 75f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem mainThrusterParticles;

    //Cache
    Rigidbody rb;
    AudioSource audioSource;

    //State


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if (!audioSource.isPlaying) 
            {
                audioSource.PlayOneShot(mainEngine, 0.75f);
            }
            
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!mainThrusterParticles.isPlaying)
            {
                mainThrusterParticles.Play();
            }
        }
        else 
        {
            audioSource.Pause();
            mainThrusterParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(rotateCoef);
            
            if (!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-rotateCoef);

            if (!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
        }
        else
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    private void Rotate(float coefficient)
    {
        rb.freezeRotation = true;
        transform.Rotate(coefficient * Vector3.forward * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
