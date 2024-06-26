﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsAnimationsController : MonoBehaviour
{
    [SerializeField] private GameObject settingsBox;
    [SerializeField] private GameObject shadowBackground;
    private bool _animationProgress;
    private GameObject[] _belts;

    private void Start()
    {
        _belts = GameObject.FindGameObjectsWithTag("Belt");
    }

    public void OpenSettings()
    {
        if (_animationProgress) return;
        _animationProgress = true;
        shadowBackground.transform.GetComponent<Image>().DOFade(0.5f, 1);
        settingsBox.transform.DOLocalMoveY(760, 0);
        ObjectExtension.GetSavedObjects()[0].transform.GetComponent<AudioSource>().DOPitch(0.8f, 1);
        settingsBox.transform.DOLocalMoveY(520, 1)
            .OnComplete(() =>
            {
                foreach (var belt in _belts) belt.gameObject.GetComponent<SwapBeltz>().enabled = false;
                _animationProgress = false;
            });
    }

    public void CloseSettings()
    {
        if (_animationProgress) return;
        _animationProgress = true;
        shadowBackground.transform.GetComponent<Image>().DOFade(0, 1);
        settingsBox.transform.DOLocalMoveY(520, 0);
        ObjectExtension.GetSavedObjects()[0].transform.GetComponent<AudioSource>().DOPitch(1, 1);
        settingsBox.transform.DOLocalMoveY(760, 1)
            .OnComplete(() =>
            {
                foreach (var belt in _belts) belt.gameObject.GetComponent<SwapBeltz>().enabled = true;
                shadowBackground.SetActive(false);
                gameObject.SetActive(false);
                _animationProgress = false;
            });
    }
}