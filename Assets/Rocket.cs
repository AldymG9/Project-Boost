using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float shipThrust = 10f;

    AudioSource audioSource;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }


    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                print("OK"); //todo remove print
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("DEAD");
                break;
        }
    }
    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            audioSource.UnPause();
            rigidBody.AddRelativeForce(Vector3.up * shipThrust);
        }
        else
        {
            audioSource.Pause();
        }
    }
    void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        rigidBody.freezeRotation = false;
    }

}