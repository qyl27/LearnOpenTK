using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LearnOpenTK.Utilities;

namespace LearnOpenTK.Renderer
{
    public class Vertexes
    {
        private int Dimensions = 3;
        private List<float> Vertices = new();
        
        public Vertexes()
        {
        }

        public Vertexes(int dimensions) 
            : base()
        {
            Dimensions = dimensions;
        }

        public int Length => Vertices.Count;

        public Vertexes Vertex(float x, float y, float z)
        {
            Vertices.Add(x);
            Vertices.Add(y);
            Vertices.Add(z);
            
            return this;
        }

        public Vertexes TexCoord(float u, float v)
        {
            Vertices.Add(u);
            Vertices.Add(v);
            
            return this;
        }
        
        public float[] ToArray()
        {
            return Vertices.ToArray();
        }

        public int GetSize()
        {
            return Vertices.Count * sizeof(float);
        }
    }
}