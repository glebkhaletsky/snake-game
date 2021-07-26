using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector2Int direction;
    [Header("Vectors")]
    [SerializeField] private List<Transform> parts = new List<Transform>();
    [SerializeField] private List<Vector2Int> positions = new List<Vector2Int>();
    private readonly float _timer = 0.2f;

    [SerializeField] private Vector2Int pioneerPosition;
    [Header("Part Prefab")]
    [SerializeField] private GameObject partPrefab;
    [SerializeField] private ParticleSystem fruitEffect;
    [Header("Fruit Generator")]
    [SerializeField] private FruitCreator fruitCreator;
    [SerializeField] private FruitCounter fruitCounter;
    [Header("Materials")]
    [SerializeField] private Material colorRed;
    [SerializeField] private Material colorGreen;
    [Header("UI")]
    [SerializeField] private GameObject dieScreen;
    [SerializeField] private SwipeDetection swipeDetection;


    private void Start()
    {

        dieScreen.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(MoveCell());

        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].GetComponent<Renderer>().sharedMaterial = colorGreen;
        }

        swipeDetection.SwipeEvent += OnSwipe;
    }

    private void OnSwipe(Vector2 swipeDirection)
    {
        SetDirection(swipeDirection);
    }
    public void SetDirection(Vector2 swipeDirection)
    {
        if (swipeDirection == Vector2Int.right)
        {
            if (direction != Vector2Int.left)
            {
                direction = Vector2Int.right;
            }
        }

        if (swipeDirection == Vector2Int.left)
        {
            if (direction != Vector2Int.right)
            {
                direction = Vector2Int.left;
            }
        }

        if (swipeDirection == Vector2Int.up)
        {
            if (direction != Vector2Int.down)
            {
                direction = Vector2Int.up;
            }
        }

        if (swipeDirection == Vector2Int.down)
        {
            if (direction != Vector2Int.up)
            {
                direction = Vector2Int.down;
            }
        }
    }
   
    private IEnumerator MoveCell()
    {
        yield return new WaitForSeconds(_timer);
        pioneerPosition += direction;

        positions.Insert(0, pioneerPosition);
        positions.RemoveAt(positions.Count - 1);

        for (int i = 0; i < positions.Count; i++)
        {
            parts[i].position = new Vector3(positions[i].x, positions[i].y, 0f);
        }

        if (CheckSelfIntersection(pioneerPosition) == true)
        {
            PartsDie();
        }
        if (pioneerPosition.x < 0 || pioneerPosition.x >= 10 || pioneerPosition.y < 0 || pioneerPosition.y >= 20)
        {
            PartsDie();
        }
        fruitCreator.CheckFruit(pioneerPosition);
        StartCoroutine(MoveCell());
    }

    private void PartsDie()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].GetComponent<Renderer>().sharedMaterial = colorRed;
        }

        dieScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    private bool CheckSelfIntersection(Vector2Int position)
    {
        for (int i = 1; i < positions.Count; i++)
        {
            if (positions[i] == position)
            {
                return true;
            }
        }
        return false;
    }

    public void AddPart()
    {
        Instantiate(fruitEffect, new Vector3(pioneerPosition.x, pioneerPosition.y,-1f) , Quaternion.Euler(0,-180,0));
        GameObject newPart = Instantiate(partPrefab);
        parts.Add(newPart.transform);
        Vector2Int position = positions[positions.Count - 1];
        positions.Add(position);
        newPart.transform.position = new Vector3(position.x, position.y, 0f);
        fruitCounter.AddFruit(1);
    }
}
