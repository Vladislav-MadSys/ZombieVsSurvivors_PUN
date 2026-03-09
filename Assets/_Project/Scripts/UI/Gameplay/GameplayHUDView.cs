using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDView : MonoBehaviour
    {
        [SerializeField] private Image HpBar;
        
        private GameplayHUDPresenter _presenter;
        
        public void Initialize(GameplayHUDPresenter presenter)
        {
            _presenter = presenter;    
        }
        
        public void ChangeHpBar(float percentage)
        {
            HpBar.fillAmount = percentage;
        }
    }
}
