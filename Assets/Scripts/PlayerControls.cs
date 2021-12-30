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
        processTranslation();
        processRotation();
    }
    
    void processRotation()
    {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        //this is to simulate real life movement, when
        //the player move up/down the rocket is slowly tilting up/down too
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }
    
    void processTranslation()
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
