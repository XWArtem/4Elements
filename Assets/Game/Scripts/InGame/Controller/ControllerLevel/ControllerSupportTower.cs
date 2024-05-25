using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionDirection
{
    top,
    left, 
    right, 
    bottom
}

public class ControllerSupportTower : MonoBehaviour
{
    private Dictionary<ConnectionDirection, bool> connections = new Dictionary<ConnectionDirection, bool>()
    {
        { ConnectionDirection.top, false },
        { ConnectionDirection.left, false },
        { ConnectionDirection.right, false },
        { ConnectionDirection.bottom, false },
    };

    [SerializeField] private Sprite powerSupportTowerSprite;
    [SerializeField] private Sprite distanceSupportTowerSprite;
    [SerializeField] private Sprite speedSupportTowerSprite;

    [SerializeField] private SpriteRenderer bridgeTop, bridgeLeft, bridgeRight, bridgeBottom;

    //[SerializeField] private CircleCollider2D triggerZone;

    //private ControllerTowerWeapon weapon;

    private SpriteRenderer spriteRenderer;

    private int towerLevel;
    internal SupportTowerType Type;

    private float animationLength = 0.3f;
    private TowerBase towerBase;

    private const string EnemyTag = "Enemy";

    private int X, Y;

    public void Init(SupportTowerType type, int X, int Y, int orderInLayer)
    {
        Type = type;
        transform.localScale = new Vector2(0.6f, 0.6f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite(type);
        spriteRenderer.sortingOrder = orderInLayer;

        this.X = X;
        this.Y = Y;

        bridgeTop.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer - 1;
        bridgeLeft.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer - 1;
        bridgeRight.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer - 1;
        bridgeBottom.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer + 1;

        InstallAllConnections();

        TowerPlacementAnimation();
    }

    private void OnEnable()
    {
        ControllerGrid.OnTowersFieldChanged += InstallAllConnections;
    }

    private void OnDisable()
    {
        ControllerGrid.OnTowersFieldChanged -= InstallAllConnections;
    }

    private void InstallAllConnections()
    {
        if (ControllerGrid.Instance.CheckSupportTowerConnection(X - 1, Y) != connections[ConnectionDirection.top])
        {
            connections[ConnectionDirection.top] = ControllerGrid.Instance.CheckSupportTowerConnection(X - 1, Y);
            ControllerGrid.Instance.SetSupportInjection(X - 1, Y, Type, connections[ConnectionDirection.top]); // inject
            Debug.Log("Changes in connection top!");
        }
        if (ControllerGrid.Instance.CheckSupportTowerConnection(X, Y - 1) != connections[ConnectionDirection.left])
        {
            connections[ConnectionDirection.left] = ControllerGrid.Instance.CheckSupportTowerConnection(X, Y - 1);
            ControllerGrid.Instance.SetSupportInjection(X, Y - 1, Type, connections[ConnectionDirection.left]); // inject
            Debug.Log("Changes in connection left!");
        }
        if (ControllerGrid.Instance.CheckSupportTowerConnection(X + 1, Y) != connections[ConnectionDirection.bottom])
        {
            connections[ConnectionDirection.bottom] = ControllerGrid.Instance.CheckSupportTowerConnection(X + 1, Y);
            ControllerGrid.Instance.SetSupportInjection(X + 1, Y, Type, connections[ConnectionDirection.bottom]); // inject
            Debug.Log("Changes in connection bottom!");
        }
        if (ControllerGrid.Instance.CheckSupportTowerConnection(X, Y + 1) != connections[ConnectionDirection.right])
        {
            connections[ConnectionDirection.right] = ControllerGrid.Instance.CheckSupportTowerConnection(X, Y + 1);
            ControllerGrid.Instance.SetSupportInjection(X, Y + 1, Type, connections[ConnectionDirection.right]); // inject
            Debug.Log("Changes in connection right!");
        }

        bridgeTop.gameObject.SetActive(connections[ConnectionDirection.top]);
        bridgeLeft.gameObject.SetActive(connections[ConnectionDirection.left]);
        bridgeBottom.gameObject.SetActive(connections[ConnectionDirection.bottom]);
        bridgeRight.gameObject.SetActive(connections[ConnectionDirection.right]);
    }

    private void SetSprite(SupportTowerType type)
    {
        switch (type)
        {
            case SupportTowerType.speed:
                spriteRenderer.sprite = speedSupportTowerSprite;
                break;
            case SupportTowerType.distance:
                spriteRenderer.sprite = distanceSupportTowerSprite;
                break;
            case SupportTowerType.power:
                spriteRenderer.sprite = powerSupportTowerSprite;
                break;
        }
    }

    private void TowerPlacementAnimation()
    {
        transform.DOScale(Vector2.one, animationLength).
            SetUpdate(true).
            SetEase(Ease.OutElastic);
    }
}
