using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace OceanSimulation
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private Random r = new Random();
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D OceanTexture;
        private Menu menu;
        private bool game;
        private int sCount, pCount, dCount;
        public static Operations operations;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 680;          
            sCount = r.Next(3, 6);
            pCount = r.Next(1, 3);
            dCount = r.Next(1, 2);
            game = true;           
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menu = new Menu(Content.Load<Texture2D>("menu"), Content.Load<Texture2D>("play"), Content.Load<Texture2D>("exit"), Content.Load<Texture2D>("Cursorr"), Content.Load<Texture2D>("save"), Content.Load<Texture2D>("Load"));
            OceanTexture = Content.Load<Texture2D>("аква");
            if (game ==true)
            {
                operations = new Operations(this.Content, sCount, pCount, dCount);       
            }   
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                menu.menuState = true; game = false;
            }
           if (menu.menuState == true)
            { 
                menu.Update();
                if (menu.curE == true) { this.Exit(); }
                 if (menu.play == true) { game = true; }
                 if (menu.bsave == true)
                 {
                     saving(operations.Objects);                
                 }
                 if (menu.bload == true) { operations.Objects = Loading(); }
            }
             else if (game==true)
             {
                 operations.Update();                   
             }    
                base.Update(gameTime);
            }      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (menu.menuState == false)
            {
                spriteBatch.Draw(OceanTexture, new Rectangle(0, 0, 1280, 720), Color.White);
                operations.Draw(spriteBatch);  
            }
            else   menu.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void saving(List<BaseObject> fh)
        {
            FileStream fs = new FileStream("Saves\\file.xml", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, fh);
            fs.Close();
        }//Save
        private List<BaseObject> Loading()
        {
            List<BaseObject> lst;
            using (FileStream fs = File.Open("Saves\\file.xml", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
               lst=(List<BaseObject>)formatter.Deserialize(fs);
               for (int i = 0; i < lst.Count; i++)
               {
                   if (lst[i].sc == true)
                   { lst[i].baseObject = Content.Load<Texture2D>("sfish"); }
                   else if (lst[i].pc == true)
                   { lst[i].baseObject = Content.Load<Texture2D>("pr"); }
                   else if (lst[i].dc == true)
                   { lst[i].baseObject = Content.Load<Texture2D>("som"); }
                 
               }
               return lst;
            }
        }   //Загрузка
            }
        }
    


