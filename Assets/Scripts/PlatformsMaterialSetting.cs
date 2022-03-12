using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsMaterialSetting : MonoBehaviour
{
    [SerializeField] private List<Material> primaryMaterials;
    [SerializeField] private List<Material> secondaryMaterials;
    [SerializeField] private List<Material> decorMaterials;
    [SerializeField] private List<MeshRenderer> pools;
    [SerializeField] private Transform finishParent;
    [SerializeField] private Transform decorParent;
    [SerializeField] private Transform carRoadParents;

    private void Start()
    {
        var randomIndex = Random.Range(0, primaryMaterials.Count);
        var platforms = GetComponentsInChildren<MeshRenderer>();

        var materials = new[] { primaryMaterials[randomIndex], secondaryMaterials[randomIndex]};
        foreach (var platform in platforms)
            platform.materials = materials;

        var finishes = finishParent.GetComponentsInChildren<FinishMaterialSetting>();
        foreach (var finish in finishes)
            finish.SetPlatform(randomIndex);
        
        var decor = decorParent.GetComponentsInChildren<MeshRenderer>();
        foreach (var decoration in decor)
            decoration.material = decorMaterials[randomIndex];

        foreach (var pool in pools)
            pool.material = primaryMaterials[randomIndex];
        
        var roads = carRoadParents.GetComponentsInChildren<MeshRenderer>();
        foreach (var road in roads)
            road.materials = materials;
    }
}
