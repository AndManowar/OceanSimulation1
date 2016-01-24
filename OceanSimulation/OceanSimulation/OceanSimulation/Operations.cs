using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OceanSimulation
{
    public sealed class Operations
    {
        public List<BaseObject> Objects = new List<BaseObject>();
        private Random r = new Random();
        public Operations(ContentManager Content, int sCoun, int pCount, int dCount)
        {
            FishesTogether(sCoun, pCount, dCount, Content);
        }
        private void FishesTogether(int sCount, int pCount, int dCount, ContentManager Content)
        {
            for (int i = 0; i < sCount; i++)
            {
                Objects.Add(new SimpleFish(Content.Load<Texture2D>("sfish"), new Vector2(r.Next(1180), r.Next(190)), new Vector2(r.Next(1, 2), 0), r.Next(1000, 2000), r.Next(1000,2000)));
            }
            for (int i = 0; i < pCount; i++)
            {

                Objects.Add(new PredatoryFish(Content.Load<Texture2D>("pr"), new Vector2(r.Next(1180), r.Next(200, 490)), new Vector2(r.Next(1, 4), r.Next(1, 2)), r.Next(2000, 7000), r.Next(1000, 2500)));
            }
            for (int i = 0; i < dCount; i++)
            {

                Objects.Add(new DeepFish(Content.Load<Texture2D>("som"), new Vector2(r.Next(1180), r.Next(600, 670)), new Vector2(1, r.Next(0, 1)), r.Next(2000, 7000), r.Next(2000, 5000)));
            }

        }//Сбор всех рыб.
        private void Intersect()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                for (int j = 0; j < Objects.Count; j++)
                    if (Objects[i].meet == true && Objects[j].meet == false)
                    {
                        if (Objects[i].BaseRectangle.Intersects(Objects[j].BaseRectangle))
                        {
                            Objects.Remove(Objects[j]);
                        }

                    }
            }

        }//проверка на столкновение     
        public void Update()
        {
            for (int i = 0; i < Objects.Count; i++)
            { Objects[i].Update(); }
            Intersect();        
        }
        public void Draw(SpriteBatch sprt)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Draw(sprt);
            }
        }  
    }
}
