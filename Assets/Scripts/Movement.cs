using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust, sideThrust = 850f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem thrustParticles, leftThrustParticles, rightThrustParticles;
    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    //Updating the movement every frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    //Pushing the rocket forward
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopParticles();
        }
    }
    void StartThrusting()
    {
        //Playing audio during thrusting
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        //Forward rocket movement & VFX
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if(!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }
    void StopThrusting()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }
    void RotateLeft()
    {
        ApplyRotation(sideThrust);
        if(!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(-sideThrust);
        if(!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }
    void StopParticles()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so physics are taking over
    }
}
