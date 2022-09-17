#version 330 core
out vec4 outColor;

in vec2 tex;

uniform sampler2D texture0;

void main()
{
    outColor = texture(texture0, tex);
}