using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Renderer3D.Graphics.Engine3D
{
    public struct Vertex3D : IVertexType
    {
        public Vector3 position;
        public Color color;
        public Vector2 texCoord;
        public Vector3 normal;

        private static VertexDeclaration vertex = new VertexDeclaration((VertexElement[])(object)new VertexElement[4]
        {
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),           //(f, f, f)    | +(4 * 3) | total: 0
            new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0),               //(b, b, b, b) | +4       | total: 0 + 4 * 3 = 12
            new VertexElement(16, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0), //(f, f)       | +(4 * 2) | total: 12 + 4 = 16
            new VertexElement(24, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),            //(f, f, f)    | +(4 * 3) | total: 16 + 4 * 2 = 16 + 8 = 24
        });

        public VertexDeclaration VertexDeclaration => vertex;

        public static VertexDeclaration Declaration => vertex;

        public Vertex3D(Vector3 position, Color color, Vector2 texCoord, Vector3 normal)
        {
            normal.Normalize();

            this.position = position;
            this.color = color;
            this.texCoord = texCoord;
            this.normal = normal;
        }
    }
}
