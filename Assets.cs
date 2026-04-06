using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Renderer3D
{
    public static class Assets
    {
        public static Asset<Effect> Texturing { get; private set; } = new("Effects\\Texturing");

        public static void Load(ContentManager content)
        {
            PropertyInfo[] properties = typeof(Assets).GetProperties();
            
            foreach (PropertyInfo property in properties)
            {
                Type type = property.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Asset<>))
                {
                    var asset = property.GetValue(content, null);
                    MethodInfo loadInfo = asset.GetType().GetMethods().FirstOrDefault(i => i.Name == "Load");
                    loadInfo.Invoke(asset, new object[] { content });
                }
            }
        }
    }

    public class Asset<T> where T : class
    {
        public string Name { get; private set; }

        public T Value { get; private set; } = null;

        public bool IsLoaded { get; private set; } = false;

        public Asset(string name)
        {
            Name = name;
        }

        public void Load(ContentManager content)
        {
            if (!IsLoaded && Value == null && Name != null)
            {
                Value = content.Load<T>(Name);
            }
        }
    }
}
