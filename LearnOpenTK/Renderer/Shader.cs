using System;
using System.IO;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace LearnOpenTK.Renderer;

public class Shader : IDisposable
{
    private ProgramHandle Program;

    public Shader(string vertexPath, string fragmentPath)
    {
        var vertexSource = File.ReadAllText(vertexPath);
        var fragmentSource = File.ReadAllText(fragmentPath);

        var vertex = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertex, vertexSource);
        
        var fragment = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragment, fragmentSource);
        
        GL.CompileShader(vertex);

        var success = 0;
        GL.GetShaderi(vertex, ShaderParameterName.CompileStatus, ref success);
        if (success != (int) All.True)
        {
            GL.GetShaderInfoLog(vertex, out var log);
            Console.WriteLine(log);
        }

        GL.CompileShader(fragment);

        GL.GetShaderi(fragment, ShaderParameterName.CompileStatus, ref success);
        if (success != (int) All.True)
        {
            GL.GetShaderInfoLog(fragment, out var log);
            Console.WriteLine(log);
        }

        Program = GL.CreateProgram();
        GL.AttachShader(Program, vertex);
        GL.AttachShader(Program, fragment);
        GL.LinkProgram(Program);

        var linkResult = 0;
        GL.GetProgrami(Program, ProgramPropertyARB.LinkStatus, ref linkResult);
        if (linkResult != (int) All.True)
        {
            GL.GetProgramInfoLog(Program, out var log);
            Console.WriteLine(log);
        }

        GL.DetachShader(Program, vertex);
        GL.DetachShader(Program, fragment);
        GL.DeleteShader(fragment);
        GL.DeleteShader(vertex);
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