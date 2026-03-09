using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDPresenter
    {
        private GameplayHUDModel _model;
        private GameplayHUDView _view;
        
        public GameplayHUDPresenter(GameplayHUDModel model, GameplayHUDView view)
        {
            _model = model;
            _view = view;
        }

        public void Run()
        {
            _model.OnPlayerHpChanged += OnPlayerHPChanged;
            _model.OnPlayerExpChanged += OnPlayerExpChanged;
        }

        public void Dispose()
        {
            _model.OnPlayerHpChanged -= OnPlayerHPChanged;
            _model.OnPlayerExpChanged -= OnPlayerExpChanged;
        }

        private void OnPlayerHPChanged(float currentHp, float maxHp)
        {
            float percentage = currentHp / maxHp;
            _view.ChangeHpBar(percentage);
        }
        
        private void OnPlayerExpChanged(float avatarCurrentExp, float expToNextLevel)
        {
            float percentage = avatarCurrentExp / expToNextLevel;
            _view.ChangeExpBar(percentage);
        }
    }
}
