using StoryGeneratorEngine.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface IGame
    {
        void RunGame();
        void Tick();
        void Init();
        void EndGame(string message);
    }
}
