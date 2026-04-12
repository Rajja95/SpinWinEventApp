namespace RUS95.SpinWinEventApp.Core
{
    public interface ISpinInputReceiver
    {
        void OnSpinRequested();
        void OnSpinDrag(float delta);
    }
}