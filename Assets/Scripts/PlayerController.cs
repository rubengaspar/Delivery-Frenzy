using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text throwText;
    [SerializeField] private TMP_Text boxInfo;

    [Header("Settings")]
    [SerializeField] public float maxSpeed = 15f;
    [SerializeField] public float moveSpeed = 20f;

    [SerializeField] public float minThrowForce = 2f;
    [SerializeField] public float maxThrowForce = 50f;
    [SerializeField] public float rotationSpeed = 5f;
    [SerializeField] public float maxThrowChargeTime = 3f;

    [SerializeField] public float interactionDistance = 3f;
    
    [Header("Movement")]
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Vector3 movement;

    [Header("Containers")]
    [SerializeField]private GameObject BoxHolder;

    private GameObject lastTarget;
    private bool isHoldingBox = false;
    private GameObject heldBox;

    PlayerInputActions playerInputActions;
    private PlayerInput playerInput;
    private Rigidbody rb;

    // Throw variables
    private float throwStartTime;
    private float currentThrowStrength;
    private float throwForce;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();

        // Subscribe to actions
        playerInputActions.NormalMode.Enable();
        playerInputActions.NormalMode.Use.performed += UseAction;
        playerInputActions.NormalMode.Throw.started += StartThrow;
        playerInputActions.NormalMode.Throw.canceled += ThrowAction;

        // Start throw text hidden
        throwText.enabled = false;
        boxInfo.enabled = false;

    }

    void Start()
    {
        BoxHolder = GameObject.Find("BoxHolder");
    }

    void Update()
    {
        TargetHandler();
        DisplayBoxInfo();
    }

    private void FixedUpdate()
    {
        // get input from input action and convert to vector3
        _movement = playerInputActions.NormalMode.Movement.ReadValue<Vector2>();
        movement = new Vector3(_movement.x, 0, _movement.y);

        // apply character rotation
        if (movement != Vector3.zero) // so that we only apply rotation when keys are pressed
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        // Add force to move
        rb.AddForce(movement * moveSpeed, ForceMode.Force);

        // Limit the maximum speed the player can travel at
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }


    private bool IsGrounded()
    {
        float extraHeight = 0.2f;
        bool grounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + extraHeight);
        return grounded;
    }


    private void TargetHandler()
    {
        if (!isHoldingBox)
        {        
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool hasTarget = Physics.Raycast(ray, out hit, interactionDistance);

            if (hasTarget)
            {
                if (hit.collider.gameObject.CompareTag("Grabbable"))
                {
                    // Highlight the Object
                    UnhighlightLastTarget();
                    HighlightObject(hit.collider.gameObject);
                    lastTarget = hit.collider.gameObject;
                }
                else
                {
                    // Unhighlight any highlighted boxes
                    UnhighlightLastTarget();
                }
            }

        }
    }

    void DisplayBoxInfo()
    {
        if (isHoldingBox)
        {
            float weight = heldBox.GetComponent<Rigidbody>().mass;
            float width = heldBox.transform.localScale.x;
            float height = heldBox.transform.localScale.y;
            float length = heldBox.transform.localScale.z;

            boxInfo.enabled = true;
            boxInfo.text = $"Weight: {weight:F2}kg\nWidth: {width:F0}\nHeight: {height:F0}\nLength: {length:F0}";
        }
        else
        {
            boxInfo.enabled = false;
        }
    }

    void HighlightObject(GameObject box)
    {
        box.GetComponent<Renderer>().material.color = Color.green;
    }

    void UnhighlightLastTarget()
    {
        if (lastTarget != null)
        {
            lastTarget.GetComponent<Renderer>().material.color = Color.white;
            lastTarget = null;
        }
    }



    void UseAction(InputAction.CallbackContext context)
    {
        // Apply logic of other uses in other objects here later (swtich lights on and off)
        if (isHoldingBox)
        {
            DropAction();
        }
        else
        {
            GrabAction();
        }
    }

    void GrabAction()
    {
        if (lastTarget != null)
        {
            isHoldingBox = true;

            lastTarget.transform.SetParent(this.transform);
            lastTarget.transform.localPosition = new Vector3(0, 2, 3);
            lastTarget.GetComponent<Rigidbody>().isKinematic = true;
            heldBox = lastTarget;
        }
        else
        {
            Debug.Log("No Action to be performed");
        }
    }

    void DropAction()
    {
        isHoldingBox = false;
        heldBox.transform.SetParent(BoxHolder.transform);
        heldBox.GetComponent<Rigidbody>().isKinematic = false;

        lastTarget = heldBox;
        UnhighlightLastTarget();
        heldBox = null;
    }


    void StartThrow(InputAction.CallbackContext context)
    {
        throwText.enabled = true;
        throwStartTime = Time.time;
        StartCoroutine(ThrowTimer());
    }

    IEnumerator ThrowTimer()
    {
        while (true)
        {
            float elapsedTime = Time.time - throwStartTime;
            if (elapsedTime >= maxThrowChargeTime)
            {
                throwText.text = $"{maxThrowChargeTime}s";
                StartCoroutine(PulseText());
                yield break;
            }
            throwText.text = $"{elapsedTime:F2}s";
            yield return null;
        }
    }

    IEnumerator PulseText()
    {
        float pulseDuration = 0.5f; // 0.5 seconds for one pulse
        float minSize = 1f; // minimum scale factor
        float maxSize = 1.2f; // maximum scale factor

        throwText.color = Color.red;

        while (true)
        {
            float startTime = Time.time;

            // Scale up
            while (Time.time < startTime + pulseDuration)
            {
                float t = (Time.time - startTime) / pulseDuration;
                float size = Mathf.Lerp(minSize, maxSize, t);
                throwText.transform.localScale = new Vector3(size, size, size);
                yield return null;
            }

            startTime = Time.time;

            // Scale down
            while (Time.time < startTime + pulseDuration)
            {
                float t = (Time.time - startTime) / pulseDuration;
                float size = Mathf.Lerp(maxSize, minSize, t);
                throwText.transform.localScale = new Vector3(size, size, size);
                yield return null;
            }
        }
    }


    void ThrowAction(InputAction.CallbackContext context)
    {
        if (isHoldingBox)
        {
            float elapsedTime = Time.time - throwStartTime;
            elapsedTime = Mathf.Clamp(elapsedTime, 0, 5);
            currentThrowStrength = Mathf.Lerp(minThrowForce, maxThrowForce, elapsedTime / maxThrowChargeTime);

            // Temporarily store the heldBox reference
            GameObject tempHeldBox = heldBox;

            // Clear box state
            isHoldingBox = false;
            heldBox.transform.SetParent(BoxHolder.transform);
            heldBox.GetComponent<Rigidbody>().isKinematic = false;
            lastTarget = heldBox;
            UnhighlightLastTarget();
            heldBox = null;

            // Direction vector (45º angle to the front)
            Vector3 forward = transform.forward;
            Vector3 up = Vector3.up;
            Vector3 throwDirection = (forward + up).normalized;

            // Apply force to the box
            tempHeldBox.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);

            // Random rotation vector
            Vector3 randomImpulse = new Vector3(
                UnityEngine.Random.Range(-5, 5),
                UnityEngine.Random.Range(-5, 5),
                UnityEngine.Random.Range(-5, 5));

            // Apply rotation vector
            tempHeldBox.GetComponent<Rigidbody>().AddForce(throwDirection * currentThrowStrength, ForceMode.Impulse);

            StopCoroutine(ThrowTimer());
            StopCoroutine(PulseText());
            throwText.color = Color.white;
            throwText.enabled = false;
        
        }

    }
}
