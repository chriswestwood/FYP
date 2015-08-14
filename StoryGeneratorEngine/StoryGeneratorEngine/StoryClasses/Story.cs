using StoryGeneratorEngine.EngineClasses;
using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.GameClasses
{
    class Story : IStory
    {
        public List<ICharacter> MainCharacters = new List<ICharacter>();
        public List<ICharacter> TargetCharacters = new List<ICharacter>();
        public IPlot Plot = null;
        private string targetName = null;
        public List<IAreaType> Areas = new List<IAreaType>();

        public List<string> currentEvents = new List<string>();

        public Story(IPlot plot)
        {
            this.Plot = plot;
            setTarget();
        }
        public void addAreaType(IAreaType newLocation)
        {
            Areas.Add(newLocation);
        }

        public void addMainCharacter(ICharacter newchar)
        {
            MainCharacters.Add(newchar);
        }

        public void addEnemyCharacter(ICharacter newchar)
        {
            TargetCharacters.Add(newchar);
        }

        public void clearCharacters()
        {
           MainCharacters.Clear();
           TargetCharacters.Clear();
        }

        public void clearAreas()
        {
            Areas.Clear();
        }

        public string getStory()
        {

            string output="";
            output += Plot.getName() + "\n";
            output += "You are " + MainCharacters[0].outputData() + "\n";
            output += Plot.getDesc().ToLower() + ":\n";
            output += targetName + "\n";
            output += "They are located within the " + Areas[Areas.Count-1].getName() + "\n";
            return output;
        }
        public List<IAreaType> getAreas()
        {
            return Areas;
        }


        public List<ICharacter> getMainCharacters()
        {
            return MainCharacters;
        }


        public void addToCurrentEvents(string newEvent)
        {
            currentEvents.Add(newEvent);
        }

        public string getCurrentEvents()
        {
            string output = "Things that have happened:\n";
            foreach(string s in currentEvents)
            {
                output += s + "\n";
            }
            return output;
        }



        public void setTarget()
        {
           if(Plot.getModifier().ToLower() == "creature")
           {
               targetName = StoryGenerationClass.Instance.getCreature(Plot.getType(), Plot.getType(), Plot.getType(), 10).getName();
           }
            if(Plot.getModifier().ToLower() == "item")
            {
                targetName = StoryGenerationClass.Instance.getItem(Plot.getType(), Plot.getType(), Plot.getType(), 10).getName();
            }
        }


        public string getTargetName()
        {
            return targetName;
        }


        public IPlot getPlot()
        {
            return Plot;
        }
    }
}
