using LearnOpenTK.Renderer;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace LearnOpenTK.Core
{
    public partial class Game : GameWindow
    {
        private Shader Shader;
        private VertexArrayHandle VAO;
        private BufferHandle VBO;
        private BufferHandle EBO;
        private Vertexes Vertexes;
        private Texture Texture;

        private uint[] indices = {
            0, 1, 3,
            1, 2, 3
        };
        
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }
        
        protected override void OnLoad()
        {
            base.OnLoad();
            
            GL.ClearColor(new Color4<Rgba>(0.2f, 0.3f, 0.3f, 1.0f));

            Vertexes = new Vertexes();

            Vertexes
                .Vertex(0.5f, 0.5f, 0.0f).TexCoord(1.0f, 1.0f)
                .Vertex(0.5f, -0.5f, 0.0f).TexCoord(1.0f, 0.0f)
                .Vertex(-0.5f, -0.5f, 0.0f).TexCoord(0.0f, 0.0f)
                .Vertex(-0.5f, 0.5f, 0.0f).TexCoord(0.0f, 1.0f);
            
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, VBO);
            GL.BufferData(BufferTargetARB.ArrayBuffer, Vertexes.ToArray(), BufferUsageARB.StaticDraw);

            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTargetARB.ElementArrayBuffer, indices, BufferUsageARB.StaticDraw);
            
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);
            
            Shader = new Shader("shader.vert", "shader.frag");

            Texture = new Texture(@"1.png");

            var posIndex = (uint) GL.GetAttribLocation(Shader.Program, "pos");
            GL.VertexAttribPointer(posIndex, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(posIndex);
            
            var texIndex = (uint) GL.GetAttribLocation(Shader.Program, "tex");
            GL.VertexAttribPointer(texIndex, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(texIndex);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            Shader.Use();
            
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2d, Texture.Handle);
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, EBO);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, VBO);
            GL.BindVertexArray(VAO);
            
            // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            
            SwapBuffers();
            
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

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
            
            GL.DeleteVertexArray(VAO);
            GL.DeleteBuffer(VBO);
            GL.DeleteBuffer(EBO);
            
            Shader.Dispose();
            
            base.OnUnload();
        }
    }
}