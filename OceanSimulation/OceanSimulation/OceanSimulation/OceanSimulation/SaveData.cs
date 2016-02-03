using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace OceanSimulation
{
    class SaveData
    {
        public static void saving(BaseObject Object)
        {

            using (FileStream fs = new FileStream(@"Saves\1.txt", FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, Object);


            }
        }//Save
        public static List<BaseObject> Loading()
        {
            List<BaseObject> lst;

            using (FileStream fs = File.Open(@"Saves\1.txt", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                lst = (List<BaseObject>)formatter.Deserialize(fs);
                return lst;
            }
        }
    }
}