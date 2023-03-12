namespace JarvenFramework.UIModule
{
    public interface IPresenter
    {
        /// <summary>
        /// 视图
        /// </summary>
        IView View { get; set; }
        void Install();
        void Uninstall();
        void OnCreateCompleted();
        void OnShowStart();
        void OnShowCompleted();
        void OnHideStart();
        void OnHideCompleted();
        void OnFocus();
        void OnUnfocus();
    }
}
