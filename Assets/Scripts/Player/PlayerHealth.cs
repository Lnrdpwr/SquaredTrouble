using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maximumHealth;
    [SerializeField] private Image _healthBar;
    [SerializeField] private AnimationCurve _healthBarChangeCurve;
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private float _invincibleTime;
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject _healthBarObject;

    private int _currentHealth;
    private int _lastDamage;
    private bool _isInvincible = false;

    private void Start()
    {
        _currentHealth = _maximumHealth;
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

            _lastDamage = damage;
            _playerAnimations.SetDamageAnimation();
            _isInvincible = true;
            StartCoroutine(ChangeBar());
            StartCoroutine(Invincible());
        }
    }

    IEnumerator ChangeBar()
    {
        float changeFrom = _currentHealth + _lastDamage;
        for(float i = 0; i < 0.25f; i += Time.deltaTime)
        {
            _healthBar.fillAmount = (changeFrom - _healthBarChangeCurve.Evaluate(i * 4)) / _maximumHealth;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Invincible()
    {
        int alpha = 1;
        for(float i = 0; i < _invincibleTime; i += _invincibleTime / 5)
        {
            alpha = Mathf.Abs(alpha - 1);
            _playerRenderer.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(_invincibleTime / 5);
        }
        _playerRenderer.color = new Color(1, 1, 1, 1);
        _isInvincible = false;
    }
}
