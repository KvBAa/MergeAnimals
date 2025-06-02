using UnityEngine;

[CreateAssetMenu(fileName = "AnimalType", menuName = "Scriptable Objects/AnimalType")]
public class AnimalType : ScriptableObject
{
    public float radius = 0.5f;
    public Sprite animalSprite;
}
