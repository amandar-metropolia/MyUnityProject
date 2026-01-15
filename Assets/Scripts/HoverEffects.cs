using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler

{
    [SerializeField] float _hoverScaleIncrease = 1.1f;
    [SerializeField] float _clickScaleIncrease = 1.3f;
    [SerializeField] float _tweenEffectDuration = 0.1f;
    [SerializeField] AudioClip _hoverSound;
    [SerializeField] AudioClip _clickSound;

    AudioSource _audioSource;
    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector2.one * _clickScaleIncrease;
        LeanTween.scale(gameObject, Vector2.one, _tweenEffectDuration).setIgnoreTimeScale(true);


        if (_clickSound != null)
            _audioSource.PlayOneShot(_clickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, Vector2.one * _hoverScaleIncrease, _tweenEffectDuration)
        .setIgnoreTimeScale(true);

        if (_hoverSound != null)
            _audioSource.PlayOneShot(_hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector2.one, _tweenEffectDuration).setIgnoreTimeScale(true);
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        _audioSource.playOnAwake = false;
    }

}
