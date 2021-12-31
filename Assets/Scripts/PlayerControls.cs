using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] private float controlSpeed = 35f;
    [SerializeField] private float xRange = 12f;
    [SerializeField] private float yRange = 13f;

    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;


    float xThrow, yThrow;
    void FixedUpdate()
    {
        ProcessTranslation();
        ProcessRotation();
    }
    
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        //this is to simulate real life movements, when
        //the player move up/down the rocket is slowly tilting up/down too
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        
        //yaw is the rotation around the y-axis, here we use local position of x
        //because when we move horizontally, we should rotate around the y-axis too to simulate real life movements
        //(ex. when we go to the left, the ship's nose will turn to the left a bit too)
        float yaw = transform.localPosition.x * positionYawFactor;
        
        //roll is the rotation around the z-axis, when we go left/right, the whole body of the ship will tilt too
        //using xthrow as it is the horizontal input, which is what we need
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }
    
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        //raw is the value not screen limited
        float rawXPos = transform.localPosition.x + xOffset;
        //clamped is the value limited inside the screen
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        
        yThrow = Input.GetAxis("Vertical");
        
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange * 0.35f, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
