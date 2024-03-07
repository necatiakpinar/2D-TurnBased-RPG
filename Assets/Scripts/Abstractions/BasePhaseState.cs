using GameplayStates;

namespace Abstractions
{
    public abstract class BasePhaseState
    {
        public abstract void Start(StateInfoTransporter stateInfoTransporter);
        public abstract void End();
    }
}