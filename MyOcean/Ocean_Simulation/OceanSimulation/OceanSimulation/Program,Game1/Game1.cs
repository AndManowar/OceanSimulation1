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
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;

namespace OceanSimulation
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        private Random r;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D OceanTexture;
        private Menu MenuState;
        private bool Game;
        public static Operations operations;
        public static ResourceManager Resourses;
        public static string FilePath = "Saves\\example.txt";
        private int Simple_Count, Predatory_Count, Deep_Count,ListCount;
        public Game1()
        {
            r = new Random();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 680;
            Simple_Count = r.Next(3, 6);
            Predatory_Count = r.Next(1, 3);
            Deep_Count = r.Next(1, 2);
            Game = true;
        }
        protected override void Initialize()
        {
            base.Initialize();
          
        }
        protected override void LoadContent()
        {
            Resourses = ResourceManager.GetInstance(this.Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MenuState = new Menu(Resourses.Get_Texture("Menu_Picture"), Resourses.Get_Texture("Play"), Resourses.Get_Texture("Exit"), Resourses.Get_Texture("Cursor"), Resourses.Get_Texture("Save"), Resourses.Get_Texture("Load"));
            OceanTexture = Resourses.Get_Texture("Ocean");
            if (Game == true)
            {
                operations = new Operations(Simple_Count, Predatory_Count, Deep_Count);
            }
          
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                MenuState.Menu_State = true; Game = false;
            }
            if (MenuState.Menu_State == true)
            {
                MenuState.Update();
                if (MenuState.Exit == true) { this.Exit(); }
                if (MenuState.Bool_Play == true) { Game = true; }
                if (MenuState.Bool_Save == true)
                {
                    Save_State();
                }
                if (MenuState.Bool_Load == true)
                {
                    Load_State();
                }
            }
            else if (Game == true)
            {
                for (int i = 0; i < operations.Objects.Count; i++)
                {
                    operations.Objects[i].Update();
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (MenuState.Menu_State == false)
            {
                spriteBatch.Draw(OceanTexture, new Rectangle(0, 0, 1280, 720), Color.White);
                for (int i = 0; i < operations.Objects.Count; i++)
                {
                    operations.Objects[i].Draw(spriteBatch);
                }
            }
            else MenuState.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
       private void Save_State()
        {
            int OneTime = 0, Count = 0;
            StreamWriter stream = new StreamWriter(new FileStream("Saves\\SimulationState.txt", FileMode.Create));
            stream.WriteLine(operations.Objects.Count);
           stream.WriteLine(MenuState.Menu_State);
           stream.WriteLine(Game);
            for (int i = 0; i < operations.Objects.Count; i++)
            {
                while (Count <= i)
                {
                    while (OneTime <= 1)
                    {
                        if (File.Exists(FilePath))
                        {
                            File.Delete(FilePath);
                        }
                        OneTime++;
                    }
                    operations.Objects[i].Save_To_Stream();
                    Count++;
                }
            }
            stream.Close();
        }
        private void Load_State()
       {
           StreamReader stream = new StreamReader(new FileStream("Saves\\SimulationState.txt", FileMode.Open));
           ListCount = Convert.ToInt32(stream.ReadLine());
           MenuState.Menu_State = Convert.ToBoolean(stream.ReadLine());
           Game = Convert.ToBoolean(stream.ReadLine());
           operations.Objects.Clear();
           BaseObject Object = new BaseObject();
           Object.Load_From_Stream(ListCount);
           stream.Close();
       }
    }
}
        
    


