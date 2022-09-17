using System;
using System.IO;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace LearnOpenTK.Renderer;

public class Shader : IDisposable
{
    public ProgramHandle Program { get; }

    public ShaderHandle Vert { get; }
    public ShaderHandle Frag { get; }

    public Shader(string vertexPath, string fragmentPath)
    {
        var vertexSource = File.ReadAllText(vertexPath);
        var fragmentSource = File.ReadAllText(fragmentPath);

        Vert = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(Vert, vertexSource);
        
        Frag = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(Frag, fragmentSource);
        
        var success = 0;
        GL.CompileShader(Vert);
        GL.GetShaderi(Vert, ShaderParameterName.CompileStatus, ref success);
        if (success != (int) All.True)
        {
            GL.GetShaderInfoLog(Vert, out var log);
            Console.WriteLine(log);
        }
        GL.CompileShader(Frag);
        GL.GetShaderi(Frag, ShaderParameterName.CompileStatus, ref success);
        if (success != (int) All.True)
        {
            GL.GetShaderInfoLog(Frag, out var log);
            Console.WriteLine(log);
        }

        Program = GL.CreateProgram();
        GL.AttachShader(Program, Vert);
        GL.AttachShader(Program, Frag);
        
        var linkResult = 0;
        GL.LinkProgram(Program);
        GL.GetProgrami(Program, ProgramPropertyARB.LinkStatus, ref linkResult);
        if (linkResult != (int) All.True)
        {
            GL.GetProgramInfoLog(Program, out var log);
            Console.WriteLine(log);
        }

        GL.DetachShader(Program, Vert);
        GL.DetachShader(Program, Frag);
        GL.DeleteShader(Frag);
        GL.DeleteShader(Vert);
    }
    
    public void Use()
    {
        GL.UseProgram(Program);
    }
    
    private bool IsDisposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            GL.DeleteProgram(Program);
            IsDisposed = true;
        }
    }

    // ~Shader()
    // {
    //     GL.DeleteProgram(Program);
    // }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}