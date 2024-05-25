using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DAdaptive : MonoBehaviour
{
    public float sceneWidth = 10f;

    private Camera camera2D;

    void Start()
    {
        camera2D = GetComponent<Camera>();
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        camera2D.orthographicSize = desiredHalfHeight;
    }
}
