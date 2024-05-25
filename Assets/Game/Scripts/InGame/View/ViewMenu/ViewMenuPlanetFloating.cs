using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMenuPlanetFloating : MonoBehaviour
{
    private RectTransform rect;

    [SerializeField] private float timeCounter = 0;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int direction = 1;
    private float radius;
    [SerializeField] private float radiusMultiplier;

    private float x, y;


    private void Start()
    {
        rect = GetComponent<RectTransform>();
        radius = Mathf.Abs(rect.anchoredPosition.y) * radiusMultiplier;
    }

    private void Update()
    {
        timeCounter += Time.deltaTime * speed * direction;

        x = Mathf.Cos(timeCounter) * radius;
        y = Mathf.Sin(timeCounter) * radius;
        rect.anchoredPosition = new Vector2(x, y);
    }
}
