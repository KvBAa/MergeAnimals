using System.Collections;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private AnimalType[] animalTypes;
    [SerializeField] private CircleCollision circleCollision;

    void Start()
    {
        //StartCoroutine(autoSpawn());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpawnAnimal(mousePosition, 0);
        }
    }

    public void SpawnAnimal(Vector2 position, int tier)
    {
        if (tier < 0 || tier >= animalTypes.Length)
        {
            Debug.LogWarning("Invalid tier specified for animal spawning.");
            return;
        }
        GameObject animal = Instantiate(animalPrefab, position, Quaternion.identity);
        animal.GetComponent<AnimalData>().SetAnimalType(animalTypes[tier]);
        CircleCollider2D collider = animal.GetComponent<CircleCollider2D>();
        collider.radius = animalTypes[tier].colliderRadius;
        collider.offset = new Vector2(animalTypes[tier].colliderOffsetX, animalTypes[tier].colliderOffsetY);
        animal.GetComponent<SpriteRenderer>().sprite = animalTypes[tier].animalSprite;
        animal.transform.localScale = new Vector3(animalTypes[tier].localScale, animalTypes[tier].localScale, 1f);
        circleCollision.AddAnimal(animal.transform, animal.GetComponent<AnimalData>());
    }

    IEnumerator autoSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(.05f);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpawnAnimal(mousePosition, 0);
        }
        
    }
}
