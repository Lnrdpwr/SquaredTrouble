using UnityEngine;

public class DamagingComponent : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().DoDamage(_damage);
        }
    }
}
