using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager References")]
    [SerializeField] public GameObject _timeManager;
    [SerializeField] public GameObject _weatherManager;
    [SerializeField] public GameObject _boxSpawner;

    public TimeManager timeManager;
    public WeatherManager weatherManager;
    public BoxSpawner boxSpawner;


    void Start()
    {
        timeManager = _timeManager.GetComponent<TimeManager>();
        weatherManager = _weatherManager.GetComponent<WeatherManager>();
        boxSpawner = _boxSpawner.GetComponent<BoxSpawner>();

        // Initialize all managers
        timeManager.Initialize(this);
        weatherManager.Initialize(this);
        boxSpawner.Initialize(this);

        // Hide the mouse cursor
        Cursor.visible = false;

    }

    void Update()
    {
        // Update all managers
        timeManager.CustomUpdate();
        weatherManager.CustomUpdate();
        boxSpawner.CustomUpdate();
    }
}
