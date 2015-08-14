using StoryGeneratorEngine.EngineClasses;
using StoryGeneratorEngine.GameClasses;
using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.StoryClasses
{
    class Location : ILocation
    {
        private IArea area;
        public int x, y;
        private string AreaName;
        private List<IGameCreature> creatures= new List<IGameCreature>();
        private List<IItem> items = new List<IItem>();
        private IEvent areaEvent;
        private bool bDoneEvent = false;
        public Location(string type = null)
        {
            area = StoryGenerationClass.Instance.getArea(type);
        }

        public string outputData()
        {
            string output = AreaName + "\n" + area.outputData() + "\n" + x + "," + y + "\n";
      
            foreach (IGameCreature c in creatures)
            {
                output +=  c.getCreature().outputData() + "\n";
            }
             foreach (IItem i in items)
            {
                output += i.outputData() + "\n";
            }

            return output;
        }

         public string look()
        {
            string output = "You are currently in the " + AreaName  + ".\n"  ;
            output += "You are surrounded by a " + area.getName() + ".\n" + area.getDesc() +"\n";
      
            foreach (IGameCreature c in creatures)
            {
                output += "There is a " + c.getCreature().getName() + " in the area." + "\n";
            }
             foreach (IItem i in items)
            {
                output += "You can see a " + i.getName() + ".\n";
            }

            return output;


        }

        public List<IItem> getItems()
        {
            return items;   

        }
        public List<IGameCreature> getCreatures()
        {

            return creatures;
        }

        public IArea getArea()
        {
            return area;
        }

        public void setLocation(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }


        public void setName(string n)
        {
            AreaName = n;
        }


        public void addItem(IItem i)
        {
            items.Add(i);
        }

        public void addCreature(ICreature c)
        {
            IGameCreature newCreature = new GameCreature(c);
            creatures.Add(newCreature);
        }


        public string addNewItem(string name = null,string type = null, string areaType = null , int highestValue = 9)
        {
            IItem newItem = StoryGenerationClass.Instance.getItem(name, type, areaType, highestValue);
            items.Add(newItem);
            return newItem.getName();
        }
        public string addNewCreature(string name = null, string type = null, string areaType = null, int highestValue = 9)
        {
            IGameCreature newCreature = new GameCreature(StoryGenerationClass.Instance.getCreature(name, type, areaType, highestValue));
            creatures.Add(newCreature);
            return newCreature.getCreature().getName();
        }

        public void setEvent(IEvent e)
        {
            areaEvent = e;
        }


        public IEvent getEvent()
        {
            return areaEvent;
        }

        public bool hasDoneEvent()
        {
            return bDoneEvent;
        }
        public void setDoneEvent()
        {
            bDoneEvent = true;
        }
    }
}
