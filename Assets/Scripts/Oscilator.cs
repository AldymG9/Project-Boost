using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period =2f;
    void Start()
    {
        startingPosition = transform.position;
    }

    //Moving the obstacle
    void Update()
    {
        if (period == 0){return;}
        float cycles = Time.time / period; //continually growing over time

        const float tau = Mathf.PI * 2; //constant value of 2PI
        float rawSinWave = Mathf.Sin(cycles * tau); //-1 to 1 range

        movementFactor = (rawSinWave +1f)/2f; //clearing the range to 0 to 1
        
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
