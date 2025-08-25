using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitManager : MonoBehaviour
{

    #region Variables and References
    [Header("Elements")] //Header for the Inspector
    [SerializeField] private SkinDataSO skinData;
    [SerializeField] private LineRenderer fruitIndicatorLine;
    [SerializeField] private Transform fruitsParent;
    [SerializeField] private float spawnDelay;
    private Fruit _currentFruit;

    [Header("Settings")]
    [SerializeField] private float spawnPathYPosition;
    
    private bool canControl;
    private bool canSpawnNewFruits;
    private bool isControlling;

    [Header("NextFruitsSettings")]
    private int nextFruitIndex;    

    [Header("Debug")]
    [SerializeField] private bool enableGizmos;

    [Header("Action")] public static Action onNextFruitIndexSet;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        MergeManager.OnMergeProcessed += MergeProcessCallback;
        ShopManager.onSkinSelected += SkinSelectedCallback;
    }
    private void OnDestroy()
    {
        MergeManager.OnMergeProcessed -= MergeProcessCallback;
        ShopManager.onSkinSelected -= SkinSelectedCallback;
    }

    void Start()
    {
        SetNextFruitsIndex();
        canControl = true;
        canSpawnNewFruits = true;
        HideLine();
    }
    
    void Update()
    {
        if (!GameManager.Instance.IsGameState())
        {
            return;
        }

        if (canSpawnNewFruits)
        {
            ManagePlayerInput();
        }
    }

    #endregion

    private void SkinSelectedCallback(SkinDataSO skinDataSo)
    {
       skinData = skinDataSo ;
    }

    #region Input Handling

    private void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownCallback();
        }
        else if (Input.GetMouseButton(0))
        {
            if (isControlling)
            {
                MouseDragCallback();
            }
            else
            {
                MouseDownCallback();
            }
        }
        else if (Input.GetMouseButtonUp(0) && isControlling)
        {
            MouseUpCallback();
        }
    }

    private void MouseDownCallback()
    {
        ShowLine();
        PlaceLineAtClickedPosition();
        SpawnFruit();
        isControlling = true;
    }

    private void MouseDragCallback()
    {
        if (_currentFruit == null) return;
        PlaceLineAtClickedPosition();
        _currentFruit.MoveTo(new Vector2(GetSpawnPosition().x,
                            spawnPathYPosition));
    }

    private void MouseUpCallback()
    {
        if (_currentFruit == null) return;
        HideLine();
        if (_currentFruit != null)
        {
            _currentFruit.EnablePhysics();
        }

        
        canSpawnNewFruits = false;
        StartControlTimer();
        
        isControlling = false;
    }

    #endregion

    #region Fruit Management

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        Fruit fruitToinstatntiate = skinData.GetSpawnablePrefabs()[nextFruitIndex];
        
        _currentFruit = Instantiate(
                            fruitToinstatntiate, spawnPosition, 
                            Quaternion.identity, 
                            fruitsParent);

        SetNextFruitsIndex();
    }

    private void SetNextFruitsIndex()
    {
        nextFruitIndex = Random.Range(0, skinData.GetSpawnablePrefabs().Length); 
        onNextFruitIndexSet?.Invoke();
    }

    public string GetNextFruitName()
    {
        return skinData.GetSpawnablePrefabs()[nextFruitIndex].name;
    }

    public Sprite GetNextFruitSprite()
    {
        return skinData.GetSpawnablePrefabs()[nextFruitIndex].GetSprite();

    }

    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //providing world point which is getting converted from Screen 
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 worldClickedPosition = GetClickedWorldPosition();

        // Offset spawn position slightly *below* the line
        float yOffset = -0.5f; // adjust as needed
        worldClickedPosition.y = spawnPathYPosition + yOffset;

        return worldClickedPosition;
    }

    #endregion

    #region Line Indicator

    private void PlaceLineAtClickedPosition()
    {
            Vector2 spawnPos = GetSpawnPosition();

            spawnPos = new Vector2(spawnPos.x, spawnPos.y + 0.5f);
            fruitIndicatorLine.SetPosition(0, spawnPos);
            fruitIndicatorLine.SetPosition(1, spawnPos + Vector2.down * 50);
        
    }

    private void HideLine()
    {
        fruitIndicatorLine.enabled = false;
    }

    private void ShowLine()
    {
        fruitIndicatorLine.enabled = true;
    }

    #endregion

    #region Control Timer

    private void StartControlTimer()
    {
        Invoke("StopControlTimer", spawnDelay);
    }

    private void StopControlTimer()
    {
        canSpawnNewFruits = true;
    }

    #endregion

    private void MergeProcessCallback( FruitType fruitType, Vector2 spawnPosition)
    {
        for (int i = 0; i <skinData.GetObjectsPrefab().Length ; i++)
        {
            if (skinData.GetObjectsPrefab()[i].GetFruitType() == fruitType)
            {
                SpawnMergedFruit(skinData.GetObjectsPrefab()[i], spawnPosition);
                return;
            }
        }
        
        Debug.LogError("Merge fruit not found");
    }

    private void SpawnMergedFruit(Fruit fruit, Vector2 spawnPosition)
    {
       Fruit fruitInstance =  Instantiate(fruit, spawnPosition,Quaternion.identity,fruitsParent);
        fruitInstance.EnablePhysics();
    }
    
    #region Editor Gizmos

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-50, spawnPathYPosition, 0),
                        new Vector3(50, spawnPathYPosition, 0));
    }
#endif

    #endregion
    
    private bool HasFruitReachedSpawnLine()
    {
        foreach (Transform fruit in fruitsParent)
        {
            if (fruit.position.y >= spawnPathYPosition)
            {
                Debug.Log("Line Reached");
                return true;
            }
        }
        return false;
    }
}
