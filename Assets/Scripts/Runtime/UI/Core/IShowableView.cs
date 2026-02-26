using DG.Tweening;

namespace Assets.Scripts.Runtime.UI.Core
{
    public interface IShowableView : IView
    {
        Tween Show();
        Tween Hide();
    }
}