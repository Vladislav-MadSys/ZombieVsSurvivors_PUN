using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuHUDView : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button connectButton;
    [SerializeField] private Button createRoomButton;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject loadingPanel;

    private MainMenuHUDPresenter _presenter;
    
    private UnityAction _onConnectionButtonClick;
    private UnityAction _onCreateButtonClick;
    private UnityAction<string> _onInputFieldChanged;

    public void Initialize(MainMenuHUDPresenter presenter)
    {
        _presenter = presenter;
    }

    public void Run()
    {
        _onConnectionButtonClick = () =>
        {
            _presenter.ClickConnectionToRoom();
            menuPanel.SetActive(false);
            loadingPanel.SetActive(true);
        };
        connectButton.onClick.AddListener(_onConnectionButtonClick);

        _onCreateButtonClick = () =>
        {
            string uniqueId = Guid.NewGuid().ToString();
            _presenter.ChangingRoomName(uniqueId);
            _presenter.ClickConnectionToRoom();
            menuPanel.SetActive(false);
            loadingPanel.SetActive(true);
        };
        createRoomButton.onClick.AddListener(_onCreateButtonClick);
        
        _onInputFieldChanged = (string key) =>
        {
            _presenter.ChangingRoomName(key);
        };
        inputField.onValueChanged.AddListener(_onInputFieldChanged);
    }

    private void OnDestroy()
    {
        connectButton.onClick.RemoveListener(_onConnectionButtonClick);
        inputField.onValueChanged.RemoveListener(_onInputFieldChanged);
    }
}
