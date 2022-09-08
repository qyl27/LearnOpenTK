using System;
using LearnOpenTK.Renderer;
using LearnOpenTK.Utilities;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenTK.Core
{
    public partial class Game : GameWindow
    {
        private Shader Shader;
        private VertexArrayHandle VAO;
        private BufferHandle VBO;
        private Vertexes Vertexes;
        
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }
        
        protected override void OnLoad()
        {
            base.OnLoad();
            
            GL.ClearColor(new Color4<Rgba>(0.2f, 0.3f, 0.3f, 1.0f));

            Vertexes = new Vertexes();
            
            Vertexes.Vertex(-0.5f, -0.5f, 0.0f)
                .Vertex(0.5f, -0.5f, 0.0f)
                .Vertex(0.0f, 0.5f, 0.0f);

            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, VBO);
            var floats = Vertexes.ToArray();
            
            GL.BufferData(BufferTargetARB.ArrayBuffer, floats, BufferUsageARB.StaticDraw);
            
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float,
                false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            Shader = new Shader("shader.vert", "shader.frag");
            Shader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            Shader.Use();
            
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            
            SwapBuffers();
            
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // if (KeyboardState.IsKeyDown(Keys.Escape))
            // {
            //     Close();
            // }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            
            GL.Viewport(0, 0, e.Size.X, e.Size.Y);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, BufferHandle.Zero);
            GL.BindVertexArray(VertexArrayHandle.Zero);
            GL.UseProgram(ProgramHandle.Zero);
            
            GL.DeleteBuffer(VBO);
            GL.DeleteVertexArray(VAO);
            
            Shader.Dispose();
            
            base.OnUnload();
        }
    }
}