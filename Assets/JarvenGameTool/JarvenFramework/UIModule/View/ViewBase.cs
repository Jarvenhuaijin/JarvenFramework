using System;
using UnityEngine;
using JarvenFramework.ResModule;
namespace JarvenFramework.UIModule
{
    public abstract class ViewBase<TPresenter> : IView where TPresenter : class, IPresenter
    {
        /// <summary>
        /// 根节点, 必须有CanvasGroup组件
        /// </summary>
        protected RectTransform _root;
        protected CanvasGroup _rootCanvas;
        protected TPresenter _presenter;
        private bool _isCreated = false;
        private string _resPath;
        public virtual bool Active
        {
            get
            {
                return _rootCanvas.IsActive();
            }
            set
            {
                _rootCanvas.SetActive(value);
            }
        }

        public IPresenter Presenter
        {
            get
            {
                return _presenter;
            }
            set
            {
                if (_presenter != null)
                {
                    _presenter.Uninstall();
                }

                _presenter = value as TPresenter;

                if (_presenter != null)
                {
                    _presenter.View = this;
                    _presenter.Install();
                }
            }
        }

        public ViewBase()
        {
            Presenter = IOCContainer.Resolve<TPresenter>();
        }


        /// <summary>
        /// 创建
        /// </summary>
        public void Create(Action callback = null)
        {
            if (_isCreated)
            {
                callback?.Invoke();
                return;
            }

            ParseResInfo(out string assetPath, out bool async);
            _resPath = assetPath;
            if (async)
            {
                ResManager.Instance.LoadAssetAsync<GameObject>(assetPath, obj =>
                {
                    OnGetResInfoCompleted(obj);

                });
            }
        }


        public void Preload(Action callback = null, bool instantiate = true)
        {
            throw new NotImplementedException();
        }

        public void Show(Action callback = null)
        {
            try
            {
                _presenter?.OnShowStart();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            try
            {
                OnShow(() =>
                {
                    try
                    {
                        _presenter?.OnShowCompleted();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }

                    callback?.Invoke();
                });
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void Hide(Action callback = null)
        {
            try
            {
                _presenter?.OnHideStart();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            try
            {
                OnHide(() =>
                {
                    try
                    {
                        _presenter?.OnHideCompleted();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }

                    callback?.Invoke();
                });
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void Focus()
        {
            _presenter?.OnFocus();
        }

        public void Unfocus()
        {
            _presenter?.OnUnfocus();
        }

        public void Destory()
        {
            OnDestroy();

            Presenter = null;

            GameObject.DestroyImmediate(_root.gameObject);

            ResManager.Instance.Unload(_resPath);
        }

        /// <summary>
        /// 完成创建
        /// </summary>
        protected abstract void OnCreate();

        /// <summary>
        /// 显示中
        /// </summary>
        protected virtual void OnShow(Action callback)
        {
            Active = true;

            callback?.Invoke();
        }

        /// <summary>
        /// 隐藏中
        /// </summary>
        protected virtual void OnHide(Action callback)
        {
            Active = false;

            callback?.Invoke();
        }

        /// <summary>
        /// 销毁中
        /// </summary>
        protected virtual void OnDestroy() { }

        private void OnGetResInfoCompleted(GameObject obj)
        {
            if (obj != null)
            {
                Transform parent = ParseParentAttr();

                _root = GameObject.Instantiate(obj, parent).GetComponent<RectTransform>();

                if (_root != null)
                {
                    _rootCanvas = _root.GetComponent<CanvasGroup>();
                    if (_rootCanvas == null)
                    {
                        _rootCanvas = _root.gameObject.AddComponent<CanvasGroup>();
                    }

                    OnCreate();

                    _isCreated = true;

                    _presenter?.OnCreateCompleted();
                }
                else
                {
                    throw new Exception($"## Uni Exception ## Cls:{this.GetType().Name} Func:Create Info:Instantiate failed !");
                }
            }
        }

        private void ParseResInfo(out string assetPath, out bool async)
        {
            assetPath = string.Empty;
            async = false;

            Type type = this.GetType();

            object[] attributes = type.GetCustomAttributes(typeof(ResInfoAttribute), true);

            if (attributes != null)
            {
                foreach (ResInfoAttribute attr in attributes)
                {
                    if (attr != null)
                    {
                        assetPath = attr.assetPath;
                        async = attr.async;
                    }
                }
            }

            if (string.IsNullOrEmpty(assetPath))
            {
                string name = type.Name;
                assetPath = $"Assets/Res/UI/{name}/{name}.prefab";
            }
        }

        private Transform ParseParentAttr()
        {
            Transform parent = null;
            return parent;
        }
    }
}
