using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsMaterialSetting : MonoBehaviour
{
    [SerializeField] private List<Material> primaryMaterials;
    [SerializeField] private List<Material> secondaryMaterials;
    [SerializeField] private Transform finishParent;

    private void Start()
    {
        var randomIndex = Random.Range(0, primaryMaterials.Count);
        var platforms = GetComponentsInChildren<MeshRenderer>();
        Debug.Log(randomIndex);

        var materials = new[] { primaryMaterials[randomIndex], secondaryMaterials[randomIndex]};
        foreach (var platform in platforms)
            platform.materials = materials;

        var finishes = finishParent.GetComponentsInChildren<FinishMaterialSetting>();
        foreach (var finish in finishes)
            finish.SetPlatform(randomIndex);
    }
}
