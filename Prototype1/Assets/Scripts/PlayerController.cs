using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float rpm;
    [SerializeField] float horsePower = 0;
    [SerializeField] float turnSpeed = 15;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheels;

    void Start() {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    void FixedUpdate()
    {
        if (!IsOnGround()) {
            return;
        }
        // move car forward
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f); // mph
        speedText.text = "Speed: " + speed + " mph";
        rpm = (speed % 30) * 40;
        rpmText.text = "RPM: " + rpm;
    }

    bool IsOnGround() {
        foreach (WheelCollider wheel in allWheels) {
            if (!wheel.isGrounded) {
                return false;
            }
        }
        return true;
    }
}
