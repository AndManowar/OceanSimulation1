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
        private Random r = new Random();
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D OceanTexture;
        private Menu MenuState;
        private bool Game;
        public static Operations operations;
        public static ResourseManager Resourses;
        private string FilePath = "Saves\\example.bin";
        private int Simple_Count, Predatory_Count, Deep_Count;
        public Game1()
        {
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
            Resourses = new ResourseManager(this.Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MenuState = new Menu(Resourses.Menu_Texture, Resourses.Game_Texture, Resourses.Exit_Texture, Resourses.Cursor_Texture, Resourses.Save_Texture, Resourses.Load_Texture);
            OceanTexture = Resourses.Ocean_Texture;
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
                    Serialize(operations.Objects);
                    
                }
                if (MenuState.Bool_Load == true) 
                {
                   operations.Objects= Deserialize();
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
        private void Serialize(List<BaseObject> Objects)
        {
            FileStream fstream = File.Open(FilePath, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fstream, Objects);
            fstream.Close();
        }
        private List<BaseObject> Deserialize()
        {
            List<BaseObject> Objects;
            FileStream fstream = File.Open(FilePath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Objects = (List<BaseObject>)binaryFormatter.Deserialize(fstream);
            for (int i = 0; i < Objects.Count; i++)
            {

                if (Objects[i].Simple_Type == true)
                { Objects[i].Base_Object = Game1.Resourses.Simple_Texture; }
                else if (Objects[i].Predatory_Type == true) { Objects[i].Base_Object = Game1.Resourses.Predatory_Texture; }
                else { Objects[i].Base_Object = Game1.Resourses.Deep_Texture; }
                Objects[i].Version_Control(Objects[i].CurrentVersion);//Кoнтроль версии.
            }
            fstream.Close();
            return Objects;
        }
    }
}
        
    


