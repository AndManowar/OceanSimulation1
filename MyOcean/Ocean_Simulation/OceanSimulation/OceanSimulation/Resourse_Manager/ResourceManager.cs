using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OceanSimulation
{
   public sealed class ResourceManager
    {
       public ContentManager Content;
       private static ResourceManager Resource_Manager;
       private ResourceManager(ContentManager Content)
        {
            this.Content = Content;
        }
       public static ResourceManager GetInstance(ContentManager Content)
        {                  
            if (Resource_Manager == null)
            {
                lock (typeof(ResourceManager))
                {
                    if (Resource_Manager == null)
                        Resource_Manager = new ResourceManager(Content);
                }
            }
            return Resource_Manager;
        } 
       public Texture2D Get_Texture(string Texture_Name)
       {
           return Content.Load<Texture2D>(Texture_Name);        
       }
   }
}
