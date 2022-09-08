using LearnOpenTK.Core;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

var game = new Game(
    new GameWindowSettings
    {
        // RenderFrequency = 60, UpdateFrequency = 60
    }, 
    new NativeWindowSettings
    {
        Size = new Vector2i(1280, 720), 
        Title = "Learn OpenTK!"
    }
);

game.Run();

game.Dispose();
