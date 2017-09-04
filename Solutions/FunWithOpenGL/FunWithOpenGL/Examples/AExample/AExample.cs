using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace FunWithOpenGL.Examples.AExample
{
    public class AExample : ExampleBase
    {   
        public AExample() : base("AExample", Key.A)
        {

        }

        
        public override void OnResize(object sender, EventArgs eventArgs)
        {
            GL.Viewport(0,0,Window.Width,Window.Height);
        }

        public override void Load()
        {
            
        }

        public override void Unload()
        {
            
        }

        public override void UpdateFrame(object sender, FrameEventArgs frameEventArgs)
        {
            
        }


        public override void RenderFrame(object sender, OpenTK.FrameEventArgs e)
        {
            
            GL.ClearColor(Color.DarkSlateBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

      
    }
}
