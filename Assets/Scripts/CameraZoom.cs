using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    [Header("Cinemachine Settings")]
    [SerializeField] public CinemachineVirtualCamera virtualCamera;
    [SerializeField] public float zoomSpeed = 0.1f;
    [SerializeField] public float zoomMin = 0f;
    [SerializeField] public float zoomMax = 30f;

    private CinemachineTransposer transposer;


    // Start is called before the first frame update
    void Start()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        

        if (scroll != 0)
        {
            // Offset from the scroll
            float offsetY = transposer.m_FollowOffset.y - scroll * zoomSpeed;
            float offsetZ = transposer.m_FollowOffset.z + scroll * zoomSpeed;

            // Check for offset limits
            if (offsetY > zoomMax)
            {
                offsetY = zoomMax;
            }
            else if (offsetY < zoomMin)
            {
                offsetY = zoomMin;
            }

            if (offsetZ < -zoomMax)
            {
                offsetZ = -zoomMax;
            }
            else if (offsetZ > -zoomMin)
            {
                offsetZ = -zoomMin;
            }

            transposer.m_FollowOffset.y = offsetY;
            transposer.m_FollowOffset.z = offsetZ;

        }

    
    }
}
