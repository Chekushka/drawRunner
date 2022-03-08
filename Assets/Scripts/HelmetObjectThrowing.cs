using UnityEngine;

public class HelmetObjectThrowing : MonoBehaviour
{
    [SerializeField] private float throwForce;
    private const int WallLayer = 8;
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer != WallLayer) return;
        other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwForce, ForceMode.Impulse);
    }
}
