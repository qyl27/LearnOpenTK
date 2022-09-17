using System.IO;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace LearnOpenTK.Renderer;

public class Texture
{
    public TextureHandle Handle { get; }
    
    public Texture(string path)
    {
        Handle = GL.GenTexture();
        
        StbImage.stbi_set_flip_vertically_on_load(1);

        using var stream = File.OpenRead(path);
        var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            
        GL.BindTexture(TextureTarget.Texture2d, Handle);

        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Clamp);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Clamp);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear);
        
        GL.TexImage2D(TextureTarget.Texture2d, 0, (int) InternalFormat.Rgba, image.Width, image.Height, 
            0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        GL.GenerateTextureMipmap(Handle);
    }
}