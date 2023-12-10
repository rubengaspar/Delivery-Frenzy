using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Monitoring Variables
    #region Monitoring Variables
    [Header("Monitoring")]
    [SerializeField] private Coroutine currentDashCoroutine;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float playerScore = 0;
    #endregion

    // Movement Settings
    #region Movement Settings
    [Header("Movement")]
    [SerializeField] public float maxSpeed = 15f;
    [SerializeField] public float moveSpeed = 50f;
    [SerializeField] private Vector3 movementInput;
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Vector3 movement;
    [SerializeField] private Transform cameraTransform;

    private PlayerInput playerInput;
    private Rigidbody rb;
    #endregion

    // Throw Settings
    #region Throw Settings
    [Header("Throw Settings")]
    [SerializeField] public float throwAngle = 45f;
    [SerializeField] public float minThrowForce = 2f;
    [SerializeField] public float maxThrowForce = 50f;
    [SerializeField] public float rotationSpeed = 5f;
    [SerializeField] public float maxThrowChargeTime = 1.5f;

    // Throw variables
    private float throwStartTime;
    private float currentThrowStrength;
    private float throwForce;
    #endregion

    // Interaction Settings
    #region Interaction Settings
    [Header("Interaction Settings")]
    [SerializeField] public float interactionDistance = 3f;

    private GameObject lastTarget;
    private bool isHoldingBox = false;
    private GameObject heldBox;
    #endregion

    // Dash Settings
    #region Dash Settings
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 50f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private bool isDashing = false;
    private float lastDashTime;
    #endregion

    // Other Serialized Settings and Object References
    #region Other Serialized Settings and Object References
    [Header("Holders")]
    [SerializeField] private GameObject BoxHolder;

    [Header("UI Settings")]
    [SerializeField] private TMP_Text throwText;
    [SerializeField] private TMP_Text boxInfo;

    [Header("Player Model")]
    [SerializeField] private GameObject player;
    #endregion 

    // Input System Subscribe/Unsubscribe
    #region Input System subcribe/unsubscribe
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // Enable Action MAp
        InputManager.Instance.inputActions.NormalMode.Enable();

        // Subscribe to Use
        InputManager.Instance.inputActions.NormalMode.Use.performed += UseAction;
        
        // Subscribe to Throw
        InputManager.Instance.inputActions.NormalMode.Throw.started += StartThrow;
        InputManager.Instance.inputActions.NormalMode.Throw.canceled += ThrowAction;

        // Subscribe to Dash
        InputManager.Instance.inputActions.NormalMode.Dash.performed += Dash;


        // Start throw text hidden
        throwText.enabled = false;
        boxInfo.enabled = false;

    }

    // Unsubscribe OnDestroy 
    private void OnDestroy()
    {
        // Unsubscribe from Use
        InputManager.Instance.inputActions.NormalMode.Use.performed -= UseAction;
        
        // Unsubscribe from Throw
        InputManager.Instance.inputActions.NormalMode.Throw.started -= StartThrow;
        InputManager.Instance.inputActions.NormalMode.Throw.canceled -= ThrowAction;

        // Unsubscribe from Dash
        InputManager.Instance.inputActions.NormalMode.Dash.performed -= Dash;
    }

    #endregion

    //Unity Event Lifecycle Methods
    #region Unity Event Lifecycle Methods
    void Update()
    {
        TargetHandler();
        DisplayBoxInfo();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (!IsGrounded())
        {
            rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
        }

    }

    #endregion

    // Movement Controller
    #region Movement Controller

    private void MovePlayer()
    {

        if (isDashing)
        {
            return;
        }

        if (!isDashing && currentDashCoroutine != null)
        {
            StopCoroutine(currentDashCoroutine);
        }

        // get input from input action and convert to vector3
        _movement = InputManager.Instance.GetMovementInput();
        movementInput = new Vector3(_movement.x, 0, _movement.y);

        // Creating vector based on forward and right identity
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; 
        cameraForward.Normalize();
        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        movement = cameraForward * movementInput.z + cameraRight * movementInput.x;

        // Set velocity directly instead of adding force (no acceleration or deceleration)
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);

        
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Set character rotation and move only while pressed 
        if (movement != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

    }


    public bool IsGrounded()
    {
        float extraHeight = 0.2f;
        bool grounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + extraHeight);
        return grounded;
    }
    #endregion

    // User Controller - Grab, Drop, Use
    #region Use Controller
    private void TargetHandler()
    {
        if (!isHoldingBox)
        {
            // Define the center, halfExtents, and orientation of the box
            Vector3 center = player.transform.position;
            Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);
            Quaternion orientation = player.transform.rotation;

            // Define the direction and distance
            Vector3 direction = transform.forward;
            float maxDistance = interactionDistance;

            // Perform the boxcast
            RaycastHit hit;
            bool hasTarget = Physics.BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance);

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

            // Weights
            float weight = heldBox.GetComponent<Rigidbody>().mass;

            // Dimensions
            float width = heldBox.transform.localScale.x;
            float height = heldBox.transform.localScale.y;
            float length = heldBox.transform.localScale.z;

            // Delivery Type
            float deliveryTimeRemaining = heldBox.GetComponent<BoxObject>().GetRemainingDeliveryTime();
            float deliveryPoints = heldBox.GetComponent<BoxObject>().GetCurrentPoints();

            int deliveryDaysRemaining = (int) deliveryTimeRemaining / 1440;
            int deliveryHoursRemaining = (int) ((deliveryTimeRemaining - (deliveryDaysRemaining * 1440)) / 60);
            int deliveryMinutesRemaining = (int)(deliveryTimeRemaining -
                                                        (deliveryDaysRemaining * 1440) -
                                                        (deliveryHoursRemaining * 60));

            boxInfo.enabled = true;
            
            boxInfo.text = $"Weight: {weight:F2}kg\n" +
                $"Width: {width:F2}\n" +
                $"Height: {height:F2}\n" +
                $"Length: {length:F2}\n\n" + 
                $"Deadline: {deliveryDaysRemaining:F0}d {deliveryHoursRemaining:F0}h {deliveryMinutesRemaining:F0}min\n" +
                $"Score: {deliveryPoints:F0}\n" +
                $"Color: ";
        }
        else
        {
            boxInfo.enabled = false;
        }
    }

    void HighlightObject(GameObject box)
    {
        lastTarget = box;
        Color color = box.GetComponent<BoxObject>().color;
        box.GetComponent<Renderer>().material.color = Color.yellow;       

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

    public void DropAction()
    {
        isHoldingBox = false;

        if (heldBox != null )
        {
            heldBox.transform.SetParent(BoxHolder.transform);
            heldBox.GetComponent<Rigidbody>().isKinematic = false;

            lastTarget = heldBox;
            UnhighlightLastTarget();
        }

        heldBox = null;
    }
    #endregion

    // Throw Controller - Throw
    #region Throw Controller
    void StartThrow(InputAction.CallbackContext context)
    {
        if (isHoldingBox)
        {        
            throwText.enabled = true;
            throwStartTime = Time.time;
            StartCoroutine(ThrowTimer());
        }
        else
        {
            Debug.Log("Can't throw, Not holding a box");
        }

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
            // Time elapsed to calcualte throw strength
            float elapsedTime = Time.time - throwStartTime;
            elapsedTime = Mathf.Clamp(elapsedTime, 0, maxThrowChargeTime);

            // Calculation of throw strength
            currentThrowStrength = Mathf.Lerp(minThrowForce, maxThrowForce, elapsedTime / maxThrowChargeTime);

            GameObject tempHeldBox = heldBox;

            Vector3 throwDirection = GetThrowVector();
            Rigidbody boxRb = tempHeldBox.GetComponent<Rigidbody>();

            boxRb.isKinematic = false;
            if (BoxHolder != null)
            {
                tempHeldBox.transform.SetParent(BoxHolder.transform);
            }

            boxRb.AddForce(throwDirection * currentThrowStrength, ForceMode.VelocityChange);

            if (IsInvoking("ThrowTimer"))
            {
                StopCoroutine(ThrowTimer());
            }

            if (IsInvoking("PulseText"))
            {
                StopCoroutine(PulseText());
            }

            throwText.color = Color.white;
            throwText.enabled = false;

            heldBox = null;
            tempHeldBox = null;
            isHoldingBox = false;
            throwText.enabled = false;
            lastTarget = heldBox;
            UnhighlightLastTarget();

        }
    }


    private Vector3 GetThrowVector()
        {
            return (transform.forward + transform.up * Mathf.Tan(Mathf.Deg2Rad * throwAngle)).normalized;
        }
    #endregion

    // Dash Controller - Dash
    #region Dash Controller
    private void Dash(InputAction.CallbackContext context)
    {
        if (Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            if (currentDashCoroutine != null)
            {
                StopCoroutine(currentDashCoroutine);
            }
            currentDashCoroutine = StartCoroutine(DashCoroutine());
        }
        else
        {
            lastDashTime = Time.time; // punish players when trying to dash while on cooldown
        }
    }

    private IEnumerator DashCoroutine()
    {
        // Lock double dash possibility
        isDashing = true;

        // Get direction and intensity vector and store last velocity vector
        Vector3 dash = transform.forward * dashSpeed;
        Vector3 originalVelocity = rb.velocity;


        float startTime = Time.time;

        while (Time.time < startTime + dashDuration) // Loop for dash duration
        {
            rb.velocity = new Vector3(dash.x, rb.velocity.y, dash.z);
            yield return null;
        }

        isDashing = false;
        lastDashTime = Time.time;
    }
    #endregion

    // Add Points to Score
    public void AddScore(float score)
    {
        playerScore += score;
        scoreText.text = $"SCORE: {score:F0}";
    }
}
