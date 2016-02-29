using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace OceanSimulation
{
    public sealed class Operations
    {
        public List<BaseObject> Objects = new List<BaseObject>();
        private Random r = new Random();
        public Operations() { }
        public Operations(int Simple_Count, int Predatory_Count, int Deep_Count)
        {
            FishesTogether(Simple_Count, Predatory_Count, Deep_Count);
        }
        private void FishesTogether(int Simple_Count, int Predatory_Count, int Deep_Count)
        {
            for (int i = 0; i < Simple_Count; i++)
            {
                Objects.Add(new SimpleFish(Game1.Resourses.Get_Texture("Simple"), new Vector2(r.Next(1180), r.Next(190)), new Vector2(r.Next(1, 2), 0), r.Next(1000, 2000), r.Next(1000, 2000)));
            }
            for (int i = 0; i < Predatory_Count; i++)
            {

                Objects.Add(new PredatoryFish(Game1.Resourses.Get_Texture("Predatory"), new Vector2(r.Next(1180), r.Next(200, 490)), new Vector2(r.Next(1, 4), r.Next(1, 2)), r.Next(2000, 7000), r.Next(1000, 2500)));
            }
            for (int i = 0; i < Deep_Count; i++)
            {

                Objects.Add(new DeepFish(Game1.Resourses.Get_Texture("Deep"), new Vector2(r.Next(1180), r.Next(600, 670)), new Vector2(1, r.Next(0, 1)), r.Next(2000, 7000), r.Next(2000, 5000)));
            }

        }//Сбор всех рыб.

    }
}
        
    
