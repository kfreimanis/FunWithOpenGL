using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace FunWithOpenGL.Examples
{
    public abstract class ExampleBase : IExample
    {
        protected ExampleBase(string name,Key? shortCutKey)
        {
            Name = name;
            ShortCutKey = shortCutKey;
        }

        public virtual string Name { get; }
        public virtual Key? ShortCutKey { get; }

        protected MainWindow Window;

        public virtual void LoadExample(MainWindow window)
        {
            Window = window;
            window.RenderFrame += RenderFrame;
            window.UpdateFrame += UpdateFrame;
            window.KeyUp += OnKeyUp;
            window.Resize += OnResize;
            Load();

        }

        public virtual void UnloadExample(MainWindow window)
        {
            window.RenderFrame -= RenderFrame;
            window.UpdateFrame -= UpdateFrame;
            window.KeyUp -= OnKeyUp;
            window.Resize -= OnResize;
            Unload();
        }

        public virtual void OnKeyUp(object sender, KeyboardKeyEventArgs arg)
        {
            if (arg.Key == Key.Escape)
            {
                Window.ExitCurrentExample();
            }
        }


        public abstract void OnResize(object sender, EventArgs eventArgs);
        
        public abstract void Load();
        public abstract void Unload();
        
        public abstract void UpdateFrame(object sender, FrameEventArgs frameEventArgs);
        public abstract void RenderFrame(object sender, OpenTK.FrameEventArgs e);

    }
}
