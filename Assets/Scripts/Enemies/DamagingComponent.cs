using UnityEngine;

public class DamagingComponent : MonoBehaviour
{
    //Скрипт, наносящий урон игроку
    [SerializeField] private int _damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth player))//Достаём компонент здоровья, если такой есть
        {
            player.DoDamage(_damage);
        }
    }
}
