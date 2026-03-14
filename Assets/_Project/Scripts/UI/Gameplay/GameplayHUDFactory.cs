using System;
using _Project.Scripts.GameEntities.PlayerAvatar;
using _Project.Scripts.Session;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDFactory : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private RoomSessionData _roomSessionData;
        [SerializeField] private PlayerAvatar PlayerAvatar;
        [SerializeField] private GameplayHUDView View;
        
        private GameplayHUDModel _model;
        private GameplayHUDPresenter _presenter;
        private int _previousInstanceCount = 0;

        public async void Start()
        {
            if (!PlayerAvatar.HasInputAuthority)
            {
                View.gameObject.SetActive(false);
                return;
            }
            
            await UniTask.WaitUntil(() => PlayerAvatar.IsInitialized);
            _roomSessionData = GameSceneContainer.Instance.RoomSessionData;
            _model = new GameplayHUDModel(_roomSessionData, PlayerAvatar.PlayerInstance, PlayerAvatar.States);
            _presenter = new GameplayHUDPresenter(_model, View, _roomSessionData);
            View.Initialize(_presenter, camera);
            _model.Run();
            _presenter.Run();
            View.Run();
            Debug.Log("Gameplay HUD initialized");
        }

        public void Update()
        {
            if (_roomSessionData == null) return;
            
            if (_previousInstanceCount != _roomSessionData.PlayerInstances.Count)
            {
                _previousInstanceCount = _roomSessionData.PlayerInstances.Count;
                _model.RecheckOtherPlayers();
            }
        }

        private void OnDestroy()
        {
            if (_model != null && _presenter != null)
            {
                _model.Dispose();
                _presenter.Dispose();
            }
            else
            {
                Debug.LogWarning("Gameplay HUD model or presenter not found");
            }
        }
    }
}
