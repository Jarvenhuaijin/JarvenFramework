using System;

namespace JarvenFramework.UIModule
{
    /// <summary>
    /// 视图接口
    /// </summary>
    public interface IView
    {
        IPresenter Presenter { get; set; }

        bool Active { get; set; }

        void Create(Action callback = null);
        void Preload(Action callback = null, bool instantiate = true);
        void Show(Action callback = null);
        void Hide(Action callback = null);
        void Focus();
        void Unfocus();
        void Destory();
    }
}
