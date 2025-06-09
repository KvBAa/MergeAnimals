using UnityEngine;
using System.Collections.Generic;

public class DynamicEyes : MonoBehaviour
{
    public AnimalData animalData;

    [SerializeField] private List<GameObject> eyes = new List<GameObject>();

    private float scale = 1f * 2.7f / 3;
    private float eyeMovementScale = 0.3f;

    void Awake()
    {
        if (animalData == null)
        {
            animalData = GetComponent<AnimalData>();
        }

        for (int i = 0; i < animalData.GetAnimalType().eyeCenterPositions.Length; i++)
        {
            GameObject eye = Instantiate(CommonTypes.eye, Vector3.zero, Quaternion.identity);
            eye.transform.SetParent(transform);
            eye.transform.localPosition = new Vector3(animalData.GetAnimalType().eyeCenterPositions[i].x, animalData.GetAnimalType().eyeCenterPositions[i].y, 0);
            eye.transform.localScale = new Vector3(Vector2.Distance(animalData.GetAnimalType().eyeCenterPositions[i], animalData.GetAnimalType().eyeEdgePositions[i]) * scale, Vector2.Distance(animalData.GetAnimalType().eyeCenterPositions[i], animalData.GetAnimalType().eyeEdgePositions[i]) * scale, 1f);
            
            eyes.Add(eye);

        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < eyes.Count; i++)
        {
            GameObject eye = eyes[i];
            Vector2 direction = mousePosition - (Vector2)eye.transform.position;
            eye.transform.localPosition = animalData.GetAnimalType().eyeCenterPositions[i] + RotateVectorByAngle(direction.normalized * Vector2.Distance(animalData.GetAnimalType().eyeCenterPositions[i], animalData.GetAnimalType().eyeEdgePositions[i]) * eyeMovementScale, transform.rotation.eulerAngles.z);
        }
    }

    private Vector2 RotateVectorByAngle(Vector2 vector, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(vector.x * cos - vector.y * sin, vector.x * sin + vector.y * cos);
    }
    
}
