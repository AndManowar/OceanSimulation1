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

namespace OceanSimulation
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Random r = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D OceanTexture;
        Menu menu; 
        int sCount, pCount, dCount;
       public bool game, form;
        List<BaseObject> Objects = new List<BaseObject>();
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
            form = false; 
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
                FishesTogether();
            }   
        }//Загрузка всех рыб.    
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)// "оживление".
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
                     menu.saving(Objects);
                     form = true;
                 }
                 if (menu.bload == true) { DeserializationHelp();  }
            }
             else if (game==true)
             {
                 for (int i = 0; i < Objects.Count; i++)
                 {
                     Objects[i].Update();
                 }
                 
                 Intersect();
                 RemoveAndReproduction();         
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
                for (int i = 0; i < Objects.Count; i++)
                {
                    Objects[i].Draw(spriteBatch);
                }               
            }
            else   menu.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        void RemoveAndReproduction()
        {
        
            for (int i = 0; i < Objects.Count; i++)
        {
            if (Objects[i].remove == true)
            {
                Objects.Remove(Objects[i]);
            }
            if (Objects[i].reproduction == true)
            {
                Objects.Add(new SimpleFish(Content.Load<Texture2D>("sfish"), new Vector2(r.Next(1200), r.Next(190)), new Vector2(r.Next(1, 2), 0), r.Next(1000, 5000), r.Next(1000, 2000)));
                Objects[i].reproduction = false;
                Objects[i].ReproTime = r.Next(1000, 2000);
            }
            else if (Objects[i].Preproduction == true)
            {
                Objects.Add(new PredatoryFish(Content.Load<Texture2D>("pr"), new Vector2(r.Next(1200), r.Next(200, 490)), new Vector2(r.Next(1, 4), r.Next(1, 2)), r.Next(2000, 7000), r.Next(2000, 3000)));
                Objects[i].Preproduction = false;
                Objects[i].ReproTime = r.Next(2000, 3000);
            }
            else if (Objects[i].Dreproduction == true)
            {
                Objects.Add(new DeepFish(Content.Load<Texture2D>("som"), new Vector2(r.Next(1200), r.Next(600, 670)), new Vector2(1, r.Next(0, 1)), r.Next(2000, 7000), r.Next(3000, 5000)));
                Objects[i].Dreproduction = false;
                Objects[i].ReproTime = r.Next(3000, 5000);
            }
           
        }
        }
    
        void FishesTogether()
        {
            for (int i = 0; i < sCount; i++)
            {
                Objects.Add(new SimpleFish(Content.Load<Texture2D>("sfish"), new Vector2(r.Next(1200), r.Next(190)), new Vector2(r.Next(1, 2), 0), r.Next(1000, 5000), r.Next(1000, 2000)));
            }
            for (int i = 0; i <pCount; i++)
            {

                Objects.Add(new PredatoryFish(Content.Load<Texture2D>("pr"), new Vector2(r.Next(1200), r.Next(200, 490)), new Vector2(r.Next(1, 4), r.Next(1, 2)), r.Next(2000, 7000), r.Next(1000, 2000)));
            }
            for (int i = 0; i < dCount; i++)
            {

                Objects.Add(new DeepFish(Content.Load<Texture2D>("som"), new Vector2(r.Next(1200), r.Next(600, 670)), new Vector2(1, r.Next(0, 1)), r.Next(2000, 7000), r.Next(1000, 2000)));
            }

        }//Сбор всех рыб.
        void Intersect()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                for (int j = 0; j < Objects.Count; j++)
                    if (Objects[i].meet == true && Objects[j].meet == false)
                    {
                        if (Objects[i].BaseRectangle.Intersects(Objects[j].BaseRectangle))
                        {
                            Objects.Remove(Objects[j]); sCount--;    
                        }
                       
                    }
            }
              
                }//проверка на столкновение      
        void DeserializationHelp()
        {
            Objects = menu.Loading();
            for (int i = 0; i < Objects.Count; i++)
        {
            if (Objects[i].sc == true)
            { Objects[i].baseObject = Content.Load<Texture2D>("sfish"); }
            else if (Objects[i].pc == true)
            { Objects[i].baseObject = Content.Load<Texture2D>("pr"); }
            else if (Objects[i].dc == true)
            { Objects[i].baseObject = Content.Load<Texture2D>("som"); }
            
        }
        }
      
            }
        }
    


