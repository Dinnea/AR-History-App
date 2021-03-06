using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool _isOn = false;
    public bool isOn 
    {
        get
        {
            return _isOn;
        }
    }
    [SerializeField] private RectTransform _toggleIndicator;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color _onColour;
    [SerializeField] private Color _offColour;

    private float offY;
    private float onY;
    [SerializeField] private float tweenTime = 0.25f;

    private AudioSource _audioSource;

    public delegate void ValueChange(bool value);
    public event ValueChange valueChanged;

    [SerializeField] private GameObject _AR;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _map;

    [SerializeField] private Camera _mapCamera;
    [SerializeField] private Camera _ARCamera;

    private void OnEnable()
    {
        Toggle(isOn);
    }
    private void Start()
    {
        offY = _toggleIndicator.anchoredPosition.y;
        onY = backgroundImage.rectTransform.rect.height -5.75f*_toggleIndicator.rect.height;
        _audioSource = this.GetComponent<AudioSource>();
    }

    public void Toggle(bool value, bool playSFX = true) 
    { 
        if (value != isOn) 
        {
            _isOn = value;
            ToggleColour(isOn);
            MoveIndicator(isOn);
            EnableAR(isOn);

            if (playSFX) _audioSource.Play();
            if (valueChanged != null) valueChanged(isOn);
        }
    }

    private void EnableAR(bool value)
    {
        _AR.SetActive(value);
        _mainUI.SetActive(!value);
        _map.SetActive(!value);
    }

    private void ToggleColour(bool value)
    {
        if (value) backgroundImage.DOColor(_onColour, tweenTime);
        else backgroundImage.DOColor(_offColour, tweenTime);
    }

    private void MoveIndicator(bool value)
    {
        if (value) _toggleIndicator.DOAnchorPosY(onY, tweenTime);
        else _toggleIndicator.DOAnchorPosY(offY, tweenTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn); 
    }
}
