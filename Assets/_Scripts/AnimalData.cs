using UnityEngine;

public class AnimalData : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0f;
    [SerializeField] private AnimalType animalType;

    private void Update()
    {
        lifeTime += Time.deltaTime;
    }

    public void SetAnimalType(AnimalType type)
    {
        animalType = type;
    }

    public AnimalType GetAnimalType()
    {
        return animalType;
    }
}
