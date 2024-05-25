using UnityEngine;

public class InputCameraController : MonoBehaviour
{
    [SerializeField] private Camera camera2D;

    private Vector3 position;
    private Vector2 startPos;
    private float yDelta;
    private float width;
    private float height;

    private float cameraBottomBorder;

    public static float InputZoneLeftBorder;
    public static float InputZoneRightBorder;

    private float maxDelta;

    private void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height * 10f;

        maxDelta = (float)(Screen.height / 1600f) * 0.06f;
    }

    public void Init(float cameraBottomBorder)
    {
        this.cameraBottomBorder = -cameraBottomBorder;

        InputZoneLeftBorder = GridSpawner.InputZoneLeftBorder;
        InputZoneRightBorder = GridSpawner.InputZoneRightBorder;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var pos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y));
            if (pos.x > InputZoneLeftBorder && pos.x < InputZoneRightBorder) return;

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector2 newPos = touch.position;

                yDelta = (newPos.y - startPos.y) / height;
                position = new Vector3(-newPos.x, newPos.y, 0.0f);
            }
        }
        else
        {
            if (Mathf.Abs(yDelta) > 0.005f)
            {
                yDelta /= 1.02f;
            }
            else
            {
                yDelta = 0f;
            }
        }
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(yDelta) > 0.005f)
        {
            camera2D.transform.position -= new Vector3(0f, Mathf.Clamp(yDelta, -maxDelta, maxDelta), 0f);
            camera2D.transform.position = new Vector3(camera2D.transform.position.x, Mathf.Clamp(camera2D.transform.position.y, cameraBottomBorder, 2.5f), camera2D.transform.position.z);
        }
    }
}
