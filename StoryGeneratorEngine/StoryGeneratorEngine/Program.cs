using StoryGeneratorEngine.EngineClasses;
using StoryGeneratorEngine.GameClasses;
using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoryGeneratorEngine
{
    class Program
    {
        static private IGame gameClass;
        static public StoryGenerationTool SGT;
        static private StoryGenerationClass storyGenClass;
       
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //load story from file if needed
            if (Properties.Settings.Default.bUsesStoryGenTool)
            {
                //load tool
                SGT = new StoryGenerationTool();
                //start game
                gameClass = new Game();
                gameClass.RunGame();
            }
            else
            {
                //start game
                    gameClass = new Game();
                    gameClass.RunGame();
            }
        }
        public static IGame getGameClass()
        {
            return gameClass;
        }

        public static void End()
        {
            gameClass = null;
            Application.Exit();

        }
        public static void Restart()
        {
            gameClass = null;
            Application.Restart();

        }
    }
}
