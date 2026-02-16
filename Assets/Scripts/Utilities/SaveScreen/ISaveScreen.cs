namespace Assets.Scripts.Utilities.SaveScreen
{
    public interface ISaveScreen
    {
        bool IsShown { get; }

        void Show();

        void Hide();
    }
}