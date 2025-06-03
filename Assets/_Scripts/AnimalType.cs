using UnityEngine;

[CreateAssetMenu(fileName = "AnimalType", menuName = "Scriptable Objects/AnimalType")]
public class AnimalType : ScriptableObject
{
    public int tier = 1;
    public string enviroment = "land";
    public float colliderRadius = 0.5f;
    public float localScale = 0.5f;
    public float colliderOffsetX = 0;
    public float colliderOffsetY = 0;

    public Sprite animalSprite;
}
