using Chapter0x01;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

using var game = new Game(
    new GameWindowSettings(),
    new NativeWindowSettings
    {
        Size = new Vector2i(640, 480), 
        Title = "0x01 Hello, Window!"
    }
);

game.Run();
