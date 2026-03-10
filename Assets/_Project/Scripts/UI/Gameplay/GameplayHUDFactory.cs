using System;
using _Project.Scripts.GameEntities.PlayerAvatar;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDFactory : MonoBehaviour
    {
        [SerializeField] private PlayerAvatar PlayerAvatar;
        [SerializeField] private GameplayHUDView View;
        
        private GameplayHUDModel _model;
        private GameplayHUDPresenter _presenter;
        
        public async void Start()
        {
            if (!PlayerAvatar.HasInputAuthority)
            {
                View.gameObject.SetActive(false);
            }
            
            await UniTask.WaitUntil(() => PlayerAvatar.IsInitialized);
            _model = new GameplayHUDModel(PlayerAvatar.States);
            _presenter = new GameplayHUDPresenter(_model, View);
            View.Initialize(_presenter);
            _model.Run();
            _presenter.Run();
            View.Run();
            Debug.Log("Gameplay HUD initialized");
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
