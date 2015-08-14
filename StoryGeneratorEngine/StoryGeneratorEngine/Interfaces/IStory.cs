using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.GameClasses
{
    interface IStory
    {

        void addMainCharacter(ICharacter newchar);
        void addEnemyCharacter(ICharacter newchar);
        void clearCharacters();
        void addAreaType(IAreaType newAreaType);

        List<IAreaType> getAreas();

        List<ICharacter> getMainCharacters();
        
        void clearAreas();


        void setTarget();
        void addToCurrentEvents(string newEvent);

        string getCurrentEvents();

        string getStory();

        string getTargetName();
        IPlot getPlot();
    }
}
