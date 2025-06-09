using UnityEngine;
using System.Collections.Generic;

public class CircleCollision : MonoBehaviour
{
    [SerializeField] private List<List<Pair<Transform, AnimalData>>> animalObjectsAndDatas = new List<List<Pair<Transform, AnimalData>>>();
    //[SerializeField] private List<List<AnimalData>> animalDatas = new List<AnimalData>();
    [SerializeField] private AnimalSpawner animalSpawner;

    private List<Pair<AnimalData, AnimalData>> toCollide = new List<Pair<AnimalData, AnimalData>>();
    void Start()
    {
        for (int i = 0; i < animalSpawner.GetAnimalCount(); i++)
        {
            animalObjectsAndDatas.Add(new List<Pair<Transform, AnimalData>>());
        }
    }

    void Update()
    {
        CheckCollisions();
        DoCollisions();

        //every frame write all positions of animals to file "AnimalPositions.txt" at local path
        using (System.IO.StreamWriter file = new System.IO.StreamWriter("AnimalPositions.txt", false))
        {
            for (int i = 0; i < animalObjectsAndDatas.Count; i++)
            {
                for (int j = 0; j < animalObjectsAndDatas[i].Count; j++)
                {
                    if (animalObjectsAndDatas[i][j] == null || animalObjectsAndDatas[i][j].First == null)
                        continue;
                    file.WriteLine($"{animalObjectsAndDatas[i][j].First.position.x}, {animalObjectsAndDatas[i][j].First.position.y}, {animalObjectsAndDatas[i][j].Second.GetAnimalType().name}");
                }
            }
        }
    }

    private void CheckCollisions()
    {
        return; // Disable collision checking for now, uncomment to enable
        for (int i = 0; i < animalObjectsAndDatas.Count; i++ /*every animal tier*/)
        {
            for (int j = 0; j < animalObjectsAndDatas[i].Count; j++ /*every animal in tier*/)
            {
                for (int k = j + 1; k < animalObjectsAndDatas[i].Count; k++ /*every animal in tier*/)
                {
                    if (animalObjectsAndDatas[i][j] == null || animalObjectsAndDatas[i][k] == null)
                        continue;
                    if (animalObjectsAndDatas[i][j].First == null || animalObjectsAndDatas[i][k].First == null)
                        continue;
                    float distance = Vector2.Distance(animalObjectsAndDatas[i][j].First.position, animalObjectsAndDatas[i][k].First.position);
                    if (distance <= animalObjectsAndDatas[i][j].Second.GetAnimalType().colliderRadius * animalObjectsAndDatas[i][j].Second.GetAnimalType().localScale * 2 + 0.1f)
                    {
                        toCollide.Add(new Pair<AnimalData, AnimalData>(animalObjectsAndDatas[i][j].Second, animalObjectsAndDatas[i][k].Second));
                        //HandleCollision(animalObjectsAndDatas[i][j].Second, animalObjectsAndDatas[i][k].Second, i);
                    }
                }
            }
        }
    }

    void DoCollisions()
    {
        for (int i = 0; i < toCollide.Count; i++)
        {
            if (toCollide[i] == null || toCollide[i].First == null || toCollide[i].Second == null)
                continue;
            HandleCollision(toCollide[i].First, toCollide[i].Second, toCollide[i].First.GetAnimalType().tier);
        }
        toCollide.Clear();
    }

    private void HandleCollision(AnimalData animal1, AnimalData animal2, int tier)
    {
        if (tier == animalSpawner.GetAnimalCount() - 1) return;
        animalObjectsAndDatas[animal1.GetAnimalType().tier].Remove(new Pair<Transform, AnimalData>(animal1.transform, animal1));
        animalObjectsAndDatas[animal2.GetAnimalType().tier].Remove(new Pair<Transform, AnimalData>(animal2.transform, animal2));

        //animalObjects.Remove(animal1.transform);
        //animalObjects.Remove(animal2.transform);
        //animalDatas.Remove(animal1);
        //animalDatas.Remove(animal2);
        Destroy(animal1.gameObject);
        Destroy(animal2.gameObject);
        animalSpawner.SpawnAnimal((animal1.transform.position + animal2.transform.position)/2, tier + 1);
        Debug.Log($"Collision detected between {animal1.name} and {animal2.name}");
    }

    public void AddAnimal(Transform animalTransform, AnimalData animalData, int tier)
    {
        if (animalTransform == null || animalData == null)
        {
            Debug.LogWarning("Animal Transform or Animal Data is null. Cannot add to lists.");
            return;
        }

        animalObjectsAndDatas[tier].Add(new Pair<Transform, AnimalData>(animalTransform, animalData));
        animalTransform.SetParent(transform);
    }
}

public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
};

