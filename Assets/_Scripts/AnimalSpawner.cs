using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private AnimalType mouseAnimalType;
    [SerializeField] private CircleCollision circleCollision;



    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AnimalType animalType = mouseAnimalType;
            SpawnAnimal(mousePosition, animalType);
        }
    }

    void SpawnAnimal(Vector2 position, AnimalType animalType)
    {
        GameObject animal = Instantiate(animalPrefab, position, Quaternion.identity);
        animal.GetComponent<AnimalData>().SetAnimalType(animalType);
        animal.GetComponent<CircleCollider2D>().radius = animalType.radius;
        animal.GetComponent<SpriteRenderer>().sprite = animalType.animalSprite;
        animal.transform.localScale = new Vector3(animalType.radius/ 3.2f, animalType.radius/ 3.2f, 1f);
        circleCollision.AddAnimal(animal.transform, animal.GetComponent<AnimalData>());
    }
}
