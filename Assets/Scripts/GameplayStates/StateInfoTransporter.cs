using Abstractions;

namespace GameplayStates
{
    public class StateInfoTransporter
    {
        private BaseHero _selectedHero;
        private BaseEnemy _selectedEnemy;
        private bool _isWin;
        
        public BaseHero SelectedHero => _selectedHero;
        public BaseEnemy SelectedEnemy => _selectedEnemy;
        public bool IsWin => _isWin;

        public StateInfoTransporter()
        {
            
        }
        public StateInfoTransporter(BaseHero selectedHero, BaseEnemy selectedEnemy)
        {
            _selectedHero = selectedHero;
            _selectedEnemy = selectedEnemy;
        }
        
        public StateInfoTransporter(bool isWin)
        {
            _isWin = isWin;
        }
    }
}