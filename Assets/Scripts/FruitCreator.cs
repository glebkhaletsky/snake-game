using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FruitCreator : MonoBehaviour
{
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private List<Fruit> allFruits = new List<Fruit>();
    [SerializeField] private Move move;
    private void Start()
    {
        Create();
    }

    private void Create()
    {
        GameObject newFruitObject = Instantiate(fruitPrefab);
        Vector2Int RandomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 20));
        newFruitObject.transform.localScale = Vector3.zero;
        newFruitObject.GetComponent<Fruit>().Position = RandomPosition;
        newFruitObject.transform.position = new Vector3(RandomPosition.x, RandomPosition.y, 0f);
        newFruitObject.transform.DOScale(Vector3.one, 0.7f);
        allFruits.Add(newFruitObject.GetComponent<Fruit>());
    }

    public void CheckFruit(Vector2Int position)
    {
        for (int i = 0; i < allFruits.Count; i++)
        {
            if (allFruits[i].Position == position)
            {
                Destroy(allFruits[i].gameObject);
                allFruits.RemoveAt(i);
                Create();
                move.AddPart();
            }
        }
    }
}
