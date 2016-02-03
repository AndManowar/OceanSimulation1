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
        private Menu menu;
        private bool game;
        string FileName;
        private int sCount, pCount, dCount;
        public static Operations operations;
        public BaseObject baseO;
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
            baseO = new BaseObject();
            FileName = "Saves\\Save";
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
                    saving();             
                 }
                 if (menu.bload == true) { Versionning(); }
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
                else menu.Draw(spriteBatch);
                spriteBatch.End();                  
            base.Draw(gameTime);
        }
         void saving()
        {          
            XmlSerializer formatter = new XmlSerializer(typeof(List<BaseObject>));   
           FileStream fs = new FileStream(FileName+".xml", FileMode.Create);
           StreamWriter s = new StreamWriter(fs);
           formatter.Serialize(fs, operations.Objects);
            fs.Close();
            }
        //Save
        void Versionning()
         {
           XmlReader  xmlReader = new XmlTextReader(FileName+".xml"); 
             xmlReader.Read();
             do xmlReader.Read();
             while (xmlReader.Name != "Version");
             if (xmlReader.Name == "Version")
             {
                 string attr = xmlReader.GetAttribute("Version"); 
                 xmlReader.Read(); 
                 string Version = xmlReader.Value;
                 xmlReader.Close();
            if(Version==baseO.Version)
            {
                Loading();
            }
            else { Loading(); }//Загрузить доступную версию+установить значение новых полей,или убрать их.
             }           
         }//проверка версии
        void Loading()
        {         
                XDocument doc = XDocument.Load(FileName + ".xml");
                using (XmlReader xr = doc.CreateReader())
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<BaseObject>));
                    operations.Objects = (List<BaseObject>)xs.Deserialize(xr);
                    xr.Close();
                }           
            DeserializationHelper();
        }
        void DeserializationHelper()
         {
             for (int i = 0; i < operations.Objects.Count; i++)
             {
                 if (operations.Objects[i].sc == true)
                 { operations.Objects[i].baseObject = Content.Load<Texture2D>("sfish"); }
                 else if (operations.Objects[i].pc == true)
                 { operations.Objects[i].baseObject = Content.Load<Texture2D>("pr"); }
                 else if (operations.Objects[i].dc == true)
                 { operations.Objects[i].baseObject = Content.Load<Texture2D>("som"); }

             }
         }
        }   //Загрузка
            }
        
    


