using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace OceanSimulation
{
    
   sealed class Menu
    {
        Texture2D Menu_Texture, Save_Texture, Load_Texture, Game_Texture, Exit_Texture, Cursor_Texture;
        Rectangle Menu_Rectangle, Game_Rectangle, Cursor_Rectangle, Exit_Rectangle, Save_Rectangle, Load_Rectangle;
        Vector2 mousePosition, mouseSpeed;
        public bool Menu_State, Exit, Bool_Save, Bool_Load, Bool_Play;
        public Menu(Texture2D Menu_Texture, Texture2D Game_Texture, Texture2D Exit_Texture, Texture2D Cursor_Texture, Texture2D Save_Texture, Texture2D Load_Texture)
        {
            this.Menu_Texture = Menu_Texture;
            this.Game_Texture = Game_Texture;
            this.Exit_Texture = Exit_Texture;
            this.Cursor_Texture = Cursor_Texture;
            this.Load_Texture = Load_Texture;
            this.Save_Texture = Save_Texture;
            Menu_Rectangle = new Rectangle(0, 0, 1280, 720);
            Game_Rectangle = new Rectangle(600, 100, Game_Texture.Width, Game_Texture.Height);
            Exit_Rectangle = new Rectangle(600, 400, Exit_Texture.Width, Exit_Texture.Height);
            Save_Rectangle = new Rectangle(450, 200, Save_Texture.Width, Save_Texture.Height);
            Load_Rectangle = new Rectangle(450, 300, Load_Texture.Width, Load_Texture.Height);
            Menu_State = true;
            Exit = false;
            mouseSpeed.X = 1.5f;
            mouseSpeed.Y = 1.5f;
        }
        public void Update()
        {
            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Cursor_Rectangle = new Rectangle(Convert.ToInt32(mousePosition.X + mouseSpeed.X), Convert.ToInt32(mousePosition.Y + mouseSpeed.Y), Cursor_Texture.Width, Cursor_Texture.Height);
            if (Cursor_Rectangle.Intersects(Game_Rectangle) && Mouse.GetState().LeftButton == ButtonState.Pressed) { Menu_State = false; Bool_Play = true; Bool_Save = false; Bool_Load = false; }
            else if (Cursor_Rectangle.Intersects(Exit_Rectangle) && Mouse.GetState().LeftButton == ButtonState.Pressed) { Exit = true; }
            else if (Cursor_Rectangle.Intersects(Save_Rectangle) && Mouse.GetState().LeftButton == ButtonState.Pressed) { Bool_Save = true; Bool_Load = false; Bool_Play = false; }
            else if (Cursor_Rectangle.Intersects(Load_Rectangle) && Mouse.GetState().LeftButton == ButtonState.Pressed) { Bool_Load = true; Bool_Save = false; Bool_Play = false; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Menu_State == true)
            {
                spriteBatch.Draw(Menu_Texture, Menu_Rectangle, Color.White);
                spriteBatch.Draw(Game_Texture, Game_Rectangle, Color.White);
                spriteBatch.Draw(Exit_Texture, Exit_Rectangle, Color.White);
                spriteBatch.Draw(Load_Texture, Load_Rectangle, Color.White);
                spriteBatch.Draw(Save_Texture, Save_Rectangle, Color.White);
                spriteBatch.Draw(Cursor_Texture, mousePosition, Color.White);
            }

        }
        
    }
}
    

