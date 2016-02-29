using System;
using System.Windows.Forms;

namespace OceanSimulation
{
#if WINDOWS || XBOX
    static class Program
    {[STAThreadAttribute]
        static void Main(string[] args)
        {
            Game1 game = new Game1(); 
            game.Run();
          

              
           
              
             
                
            
         
          
        }
    }
#endif
}

