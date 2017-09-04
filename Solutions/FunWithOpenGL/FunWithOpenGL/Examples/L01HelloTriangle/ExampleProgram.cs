using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace FunWithOpenGL.Examples.L01HelloTriangle
{
    public class ExampleProgram  : ExampleBase
    {
        public ExampleProgram() : base("Learn opengl 01 Hello Triangle", Key.Number1)
        {
        }

        public override void OnResize(object sender, EventArgs eventArgs)
        {
            GL.Viewport(0,0,Window.Width,Window.Height);
        }

        private int _vboBuffer;
        private int _program;
        private int _vertexArray;

        public override void Load()
        {
         

            float[] vertices = {
                -0.5f, -0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                0.0f,  0.5f, 0.0f
            };

         

            var vsShaderId = CreateShader(ShaderType.VertexShader, File.ReadAllText(@"Examples\L01HelloTriangle\Shaders\vs.vert"));
            var fsShaderId = CreateShader(ShaderType.FragmentShader, File.ReadAllText(@"Examples\L01HelloTriangle\Shaders\fs.frag"));

            _program = CreateProgram(vsShaderId, fsShaderId);

            GL.DeleteShader(vsShaderId);
            GL.DeleteShader(fsShaderId);
            
            GL.UseProgram(_program);

            _vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArray);

            _vboBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboBuffer);
            GL.BufferData<float>(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float,false,3 * sizeof(float),0);
            GL.EnableVertexAttribArray(0);


            GL.BindVertexArray(0);

        }

        private int CreateProgram(int vsShaderId, int fsShaderId)
        {
            var program = GL.CreateProgram();
            GL.AttachShader(program, vsShaderId);
            GL.AttachShader(program, fsShaderId);
            GL.LinkProgram(program);

            int programSuccess;
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out programSuccess);

            if (programSuccess == 0)
            {
                var log = GL.GetProgramInfoLog(program);
                throw new Exception("Program Error\n " + log);
            }

            return program;
        }

        private static int CreateShader(ShaderType shaderType, string shaderSource)
        {
            var vsShaderId = GL.CreateShader(shaderType);
            GL.ShaderSource(vsShaderId, shaderSource);
            GL.CompileShader(vsShaderId);

            int vsCompileState;
            GL.GetShader(vsShaderId, ShaderParameter.CompileStatus, out vsCompileState);

            if (vsCompileState == 0)
            {
                var log = GL.GetShaderInfoLog(vsShaderId);
                throw new Exception("Shader Compile Error\n " + log);
            }

            return vsShaderId;
        }

        public override void Unload()
        {
            GL.DeleteProgram(_program);
            GL.DeleteBuffer(_vboBuffer);
            GL.DeleteVertexArray(_vertexArray);
        }

        public override void UpdateFrame(object sender, FrameEventArgs frameEventArgs)
        {

        }

        public override void RenderFrame(object sender, FrameEventArgs e)
        {
            GL.ClearColor(Color.DarkSlateBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(_program);
            GL.BindVertexArray(_vertexArray);

            GL.DrawArrays(PrimitiveType.Triangles, 0,3);

        }
    }
}
