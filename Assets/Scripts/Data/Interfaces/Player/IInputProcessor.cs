using Data.Data_Types;

namespace Data.Interfaces.Player
{
    public interface IInputProcessor
    {
        Elements CurrentElement { get; }
        void GlobalControls();
    }
}