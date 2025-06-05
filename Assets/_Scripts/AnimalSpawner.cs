using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private AnimalType[] animalTypes;
    [SerializeField] private CircleCollision circleCollision;
    [SerializeField] private Transform spawnPosition;

    void Start()
    {
        StartCoroutine(autoSpawn());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpawnNewAnimal(mousePosition, 0);
        }
    }

    private void SpawnNewAnimal(Vector2 position, int tier)
    {
        SpawnAnimal(new Vector2(position.x, spawnPosition.position.y), tier);
    }

    public void SpawnAnimal(Vector2 position, int tier)
    {
        if (tier < 0 || tier >= animalTypes.Length)
        {
            Debug.LogWarning("Invalid tier specified for animal spawning.");
            return;
        }
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        GameObject animal = Instantiate(animalPrefab, position, randomRotation);
        animal.GetComponent<AnimalData>().SetAnimalType(animalTypes[tier]);
        CircleCollider2D collider = animal.GetComponent<CircleCollider2D>();
        collider.radius = animalTypes[tier].colliderRadius;
        collider.offset = new Vector2(animalTypes[tier].colliderOffsetX, animalTypes[tier].colliderOffsetY);
        animal.GetComponent<SpriteRenderer>().sprite = animalTypes[tier].animalSprite;
        animal.transform.localScale = new Vector3(animalTypes[tier].localScale, animalTypes[tier].localScale, 1f);
        circleCollision.AddAnimal(animal.transform, animal.GetComponent<AnimalData>(), tier);
    }

    public int GetAnimalCount()
    {
        return animalTypes.Length;
    }

    IEnumerator autoSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpawnNewAnimal(mousePosition, 0);
        }
        
    }
}
