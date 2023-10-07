using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxObject : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] public float fallSpeedMultiplier = 5f;
    [SerializeField] private float gravity = -9.8f;

    private Vector3 movement;
    private float verticalFallSpeed = 0;

    CharacterController boxController;

    void Start()
    {
        boxController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (boxController.isGrounded)
        {
            verticalFallSpeed = 0;
        }
        else
        {
            verticalFallSpeed += gravity * Time.deltaTime;
        }

        // Assign accumulated fall speed
        movement.y = verticalFallSpeed;

        boxController.Move(movement * Time.deltaTime * fallSpeedMultiplier);
    }
}
