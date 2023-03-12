using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvenFramework.UIModule
{
    public abstract class PresenterBase<TView> : IPresenter where TView : class, IView
    {
        /// <summary>
        /// 视图
        /// </summary>
        protected TView _view;

        /// <summary>
        /// 视图
        /// </summary>
        public IView View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value as TView;
            }
        }

        public void Install()
        {

        }

        public void OnCreateCompleted()
        {

        }

        public void OnFocus()
        {
             
        }

        public void OnHideCompleted()
        {
             
        }

        public void OnHideStart()
        {
             
        }

        public void OnShowCompleted()
        {
             
        }

        public void OnShowStart()
        {
             
        }

        public void OnUnfocus()
        {
             
        }

        public void Uninstall()
        {
             
        }
    }
}
