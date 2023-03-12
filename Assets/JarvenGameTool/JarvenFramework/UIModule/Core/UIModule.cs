using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvenFramework.UIModule
{    /// <summary>
     /// UI模块
     /// </summary>
    public sealed class UIModule : IUIModule
    {


        public void Enter(int viewId, Action callback = null)
        {
            throw new NotImplementedException();
        }

        public void Focus(int viewId)
        {
            throw new NotImplementedException();
        }

        public void Preload(int viewId, bool instantiate = true)
        {
            throw new NotImplementedException();
        }

        public void Quit(int viewId, Action callback = null, bool destroy = false)
        {
            throw new NotImplementedException();
        }

        public void QuitAll(Action callback = null, bool destroy = false)
        {
            throw new NotImplementedException();
        }

        public void UnFocus(int viewId)
        {
            throw new NotImplementedException();
        }
    }
}
