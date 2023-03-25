using UnityEngine;

public class DamagingComponent : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth player))
        {
            player.DoDamage(_damage);
        }
    }
}
