using UnityEngine;

public class soulfragmentcollect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Door.Instance.CollectCoin();
            Destroy(gameObject); 
        }
    }
}
