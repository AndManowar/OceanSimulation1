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
    
    class Menu
    {
        Texture2D menuTexture, save, load, cursorGame, cursorExit, cursor;
        Rectangle menu, cursorG, curs, cursorE,Save,Load;
        Vector2 mousePosition, mouseSpeed;
        public string file = "";
        public bool menuState, curE, bsave,bload,play;
      
        public Menu(Texture2D menuTexture,Texture2D cursorGame,Texture2D cursorExit,Texture2D cursor,Texture2D save,Texture2D load )
        {
            this.menuTexture=menuTexture;
            this.cursorGame = cursorGame;
            this.cursorExit = cursorExit;
            this.cursor = cursor;
            this.load = load;
            this.save = save;        
            menu = new Rectangle(0,0, 1280, 720);
            cursorG = new Rectangle(600, 100, cursorGame.Width, cursorGame.Height);
            cursorE = new Rectangle(600, 400, cursorExit.Width, cursorExit.Height);
            Save = new Rectangle(450, 200, save.Width, save.Height);
            Load = new Rectangle(450, 300, load.Width, load.Height);
            menuState = true;     
            curE = false;
            mouseSpeed.X = 1.5f;
            mouseSpeed.Y = 1.5f;
        }
        public void Update()
        {
            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            curs = new Rectangle(Convert.ToInt32(mousePosition.X + mouseSpeed.X), Convert.ToInt32(mousePosition.Y + mouseSpeed.Y), cursor.Width, cursor.Height);
            if (curs.Intersects(cursorG) && Mouse.GetState().LeftButton == ButtonState.Pressed) { menuState = false; play = true; bsave = false; bload = false; }
            else if (curs.Intersects(cursorE) && Mouse.GetState().LeftButton == ButtonState.Pressed) { curE = true; }
            else if (curs.Intersects(Save) && Mouse.GetState().LeftButton == ButtonState.Pressed) { bsave = true; bload = false; play = false; }
            else if (curs.Intersects(Load) && Mouse.GetState().LeftButton == ButtonState.Pressed) { bload = true; bsave = false; play = false; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (menuState == true)
            {
                spriteBatch.Draw(menuTexture, menu, Color.White);            
                spriteBatch.Draw(cursorGame, cursorG, Color.White);
                spriteBatch.Draw(cursorExit, cursorE, Color.White);               
                spriteBatch.Draw(load, Load, Color.White);
                spriteBatch.Draw(save, Save, Color.White);
                spriteBatch.Draw(cursor, mousePosition, Color.White);
            }

        }
        public void saving(List<BaseObject> fh)
        {
                FileStream fs = new FileStream("Saves\\file.xml", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, fh);    
                fs.Close();                   
        }
        public List<BaseObject> Loading()
        {
            using (FileStream fs = File.Open("Saves\\file.xml", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<BaseObject>)formatter.Deserialize(fs);
            }
        }   
    }
}
    

