using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OceanSimulation
{
   public class ResourseManager
    {
       public Texture2D Simple_Texture,
           Predatory_Texture,
           Deep_Texture,
           Menu_Texture,
           Save_Texture,
           Load_Texture,
           Game_Texture,
           Exit_Texture,
           Cursor_Texture,
           Ocean_Texture;
       public ResourseManager(ContentManager Content)
        {
            Simple_Texture = Content.Load<Texture2D>("Simple");
            Predatory_Texture = Content.Load<Texture2D>("Predatory");
            Deep_Texture = Content.Load<Texture2D>("Deep");
            Menu_Texture = Content.Load<Texture2D>("Menu_Picture");
            Save_Texture = Content.Load<Texture2D>("Save");
            Load_Texture = Content.Load<Texture2D>("Load");
            Game_Texture = Content.Load<Texture2D>("Play");
            Exit_Texture = Content.Load<Texture2D>("Exit");
            Cursor_Texture = Content.Load<Texture2D>("Cursor");
            Ocean_Texture = Content.Load<Texture2D>("Ocean");
        }
    }
}
