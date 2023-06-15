using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maximumHealth;
    [SerializeField] private Image _healthBar;
    [SerializeField] private AnimationCurve _healthBarChangeCurve;//Кривая для изменения полосы здоровья(см. инспектор). По желанию можно убрать, а вместо ChangeBar отнимать здоровье и сделать _healthBar.fillAmount = _currentHealth / _maximumHealth;
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private float _invincibleTime;//Время неуязвимости
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject _healthBarObject;//Для отключения полосы здоровья при проигрыше

    private int _currentHealth;
    private int _lastDamage;
    private bool _isInvincible = false;
    internal static PlayerHealth Instance;//internal static позволяют достать объект с этим скриптом из любого другого скрипта с помощью PlayerHealth.Instance(вернёт скрипт на игроке)

    private void Start()
    {
        _currentHealth = _maximumHealth;
        Instance = this;
    }

    public void DoDamage(int damage)
    {
        if (!_isInvincible)
        {
            _currentHealth -= damage;
            if(_currentHealth <= 0)
            {
                _levelManager.StopGame();
                gameObject.SetActive(false);
                _healthBarObject.SetActive(false);
                return;
            }

            _lastDamage = damage;//Нужно для плавного изменения полоски здоровья
            _playerAnimations.SetDamageAnimation();//Анимация получения урона
            _isInvincible = true;
            StartCoroutine(ChangeBar());
            StartCoroutine(Invincible());
        }
    }

    IEnumerator ChangeBar()
    {
        float changeFrom = _currentHealth + _lastDamage;//Какое здоровье было до получения урона
        for(float i = 0; i < 0.25f; i += Time.deltaTime)
        {
            _healthBar.fillAmount = (changeFrom - _healthBarChangeCurve.Evaluate(i * 4)) / _maximumHealth;//Плавно меняем полоску здоровья
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Invincible()
    {
        int alpha = 1;
        for(float i = 0; i < _invincibleTime; i += _invincibleTime / 5)//Игрок мигает во время неуязвимости
        {
            alpha = Mathf.Abs(alpha - 1);
            _playerRenderer.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(_invincibleTime / 5);
        }
        _playerRenderer.color = new Color(1, 1, 1, 1);
        _isInvincible = false;
    }
}
