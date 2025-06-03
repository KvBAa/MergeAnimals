using UnityEngine;
using System.Collections.Generic;

public class CircleCollision : MonoBehaviour
{
    [SerializeField] private List<Transform> animalObjects = new List<Transform>();
    [SerializeField] private List<AnimalData> animalDatas = new List<AnimalData>();
    [SerializeField] private AnimalSpawner animalSpawner;
    void Start()
    {
        
    }

    void Update()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        for (int i = 0; i < animalObjects.Count; i++)
        {
            for (int j = i + 1; j < animalObjects.Count; j++)
            {
                if (animalObjects[i] == null || animalObjects[j] == null)
                    continue;

                if (animalDatas[i].GetAnimalType() != animalDatas[j].GetAnimalType())
                    continue;
                
                float distance = Vector2.Distance(animalObjects[i].position, animalObjects[j].position);
                if (distance <= animalDatas[i].GetAnimalType().colliderRadius * animalDatas[i].GetAnimalType().localScale * 2 + 0.01f)
                {
                    HandleCollision(animalDatas[i], animalDatas[j], animalDatas[i].GetAnimalType().tier);
                }
            }
        }
    }

    private void HandleCollision(AnimalData animal1, AnimalData animal2, int tier)
    {
        animalObjects.Remove(animal1.transform);
        animalObjects.Remove(animal2.transform);
        animalDatas.Remove(animal1);
        animalDatas.Remove(animal2);
        Destroy(animal1.gameObject);
        Destroy(animal2.gameObject);
        animalSpawner.SpawnAnimal((animal1.transform.position + animal2.transform.position)/2, tier + 1);
        Debug.Log($"Collision detected between {animal1.name} and {animal2.name}");
    }

    public void AddAnimal(Transform animalTransform, AnimalData animalData)
    {
        if (animalTransform == null || animalData == null)
        {
            Debug.LogWarning("Animal Transform or Animal Data is null. Cannot add to lists.");
            return;
        }

        if (animalDatas.Count != animalObjects.Count)
        {
            Debug.LogError("Fuckin nie rowna ilosc animal datas i animal objects");
            return;
        }

        animalObjects.Add(animalTransform);
        animalDatas.Add(animalData);
        animalTransform.SetParent(transform);
    }
}
