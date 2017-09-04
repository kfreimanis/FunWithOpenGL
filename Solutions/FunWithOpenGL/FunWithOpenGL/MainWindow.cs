using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace FunWithOpenGL
{
    public class MainWindow : GameWindow
    {
        private readonly bool _debug;
        private readonly IEnumerable<IExample> _examples;
        private readonly Dictionary<Key, IExample> _shortCutsExamples;

        private IExample _currentExample;

        
        public MainWindow(IEnumerable<IExample> examples,bool debug) 
            : base(800, 600
                  , GraphicsMode.Default
                  , "Fun With Open GL Sampels"
                  , GameWindowFlags.Default
                  , DisplayDevice.Default
                  , 4, 3
                  , debug ? (GraphicsContextFlags.Debug | GraphicsContextFlags.ForwardCompatible) : GraphicsContextFlags.ForwardCompatible)
        {
            _debug = debug;
            _examples = examples.OrderBy(a=>a.Name).ToList();
            _shortCutsExamples = examples.Where(a => a.ShortCutKey.HasValue).ToDictionary(a => a.ShortCutKey.Value, a => a);
        }

        protected override void OnLoad(EventArgs e)
        {
            //_currentExample = _examples.First();
            //_currentExample.LoadExample(this);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ExitCurrentExample();
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            if (_currentExample != null)
            {
                base.OnKeyUp(e);
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    this.Exit();
                }

                if (_shortCutsExamples.ContainsKey(e.Key))
                {
                    ExitCurrentExample();
                    _currentExample = _shortCutsExamples[e.Key];
                    _currentExample.LoadExample(this);
                }
            }
        }

        public void DebugProc(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message,
                              IntPtr userParam)
        {
            // ignore non-significant error/warning codes
            if (id == 131169 || id == 131185 || id == 131218 || id == 131204) return;
       
            StringBuilder errorStringBuilder = new StringBuilder();
            errorStringBuilder.AppendLine(Marshal.PtrToStringAnsi(message, length));
            errorStringBuilder.AppendLine(Enum.GetName(typeof(DebugSource), source));
            errorStringBuilder.AppendLine(Enum.GetName(typeof(DebugType), type));
            errorStringBuilder.AppendLine(Enum.GetName(typeof(DebugSeverity), severity));
            

            string msg = errorStringBuilder.ToString();
            Debugger.Break();
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0} " + " " + _currentExample?.Name;

            if (_debug)
            {
                GL.Enable(EnableCap.DebugOutput);
                GL.Enable(EnableCap.DebugOutputSynchronous);
                GL.DebugMessageCallback<int>(DebugProc, new[] {0});
            }

            base.OnRenderFrame(e);

            if (_currentExample == null)
            {

                GL.ClearColor(Color.Aqua);
                GL.Clear(ClearBufferMask.ColorBufferBit);

            }
            SwapBuffers();
        }

        public void ExitCurrentExample()
        {
            _currentExample?.UnloadExample(this);
            _currentExample = null;
        }
    }
}
