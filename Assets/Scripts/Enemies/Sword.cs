using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour
{
    [SerializeField] private AnimationCurve _slideBackCurve;
    [SerializeField] private GameObject _shadow;
    [SerializeField] private float _timeToLand, _timeToSlideBack;
    [SerializeField] private GameObject _effect;
    [SerializeField] private GameObject _damagingZone;

    private Vector3 _target, _startPosition;
    private SpriteRenderer _shadowRenderer, _swordRenderer;

    private void Awake()
    {
        _shadowRenderer = _shadow.GetComponent<SpriteRenderer>();
        _swordRenderer = GetComponent<SpriteRenderer>();

        _target = transform.parent.position;
        _startPosition = transform.position;

        _shadow.SetActive(true);

        StartCoroutine(Land());
    }

    IEnumerator Land()
    {
        float progress = 0f;
        while (progress < _timeToLand)
        {
            progress += Time.deltaTime;
            transform.position = Vector2.Lerp(_startPosition, _target, progress / _timeToLand);
            _shadowRenderer.color = new Color(1, 1, 1, progress / _timeToLand);
            yield return new WaitForEndOfFrame();
        }
        _shadow.SetActive(false);
        Instantiate(_effect, transform.position, Quaternion.identity);
        _startPosition = transform.position;
        _target = new Vector3(transform.position.x, transform.position.y + 1);

        _damagingZone.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _damagingZone.SetActive(false);

        StartCoroutine(SlideBack());
    }

    IEnumerator SlideBack()
    {
        float progress = 0f;
        while (progress < _timeToSlideBack)
        {
            progress += Time.deltaTime;
            transform.position = Vector2.Lerp(_startPosition, _target, _slideBackCurve.Evaluate(progress / _timeToSlideBack));
            _swordRenderer.color = new Color(1, 1, 1, _timeToSlideBack - progress);
            yield return new WaitForEndOfFrame();
        }
        Destroy(transform.parent.gameObject);
    }
}
