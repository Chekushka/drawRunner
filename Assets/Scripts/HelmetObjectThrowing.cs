using UnityEngine;

public class HelmetObjectThrowing : MonoBehaviour
{
    [SerializeField] private float throwForce;
    private const int GirlLayer = 7;
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == GirlLayer) return;
        other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwForce, ForceMode.Impulse);
    }
}
