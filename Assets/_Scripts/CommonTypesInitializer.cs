using UnityEngine;

public class CommonTypesInitializer : MonoBehaviour
{
    [SerializeField] private GameObject eyePrefab;
    private void Awake()
    {
        if (CommonTypes.eye == null)
        {
            CommonTypes.eye = eyePrefab;
        }
    }
}
