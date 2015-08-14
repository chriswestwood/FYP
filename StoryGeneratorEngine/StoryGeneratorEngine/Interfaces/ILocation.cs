using StoryGeneratorEngine.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface ILocation
    {

        int getX();
        int getY();

        void setName(string n);
        IArea getArea();
        void setLocation(int x, int y);
        void setEvent(IEvent e);

        IEvent getEvent();
        bool hasDoneEvent();
        void setDoneEvent();

        void addItem(IItem i);
        void addCreature(ICreature c);
        string addNewCreature(string name = null, string type = null, string areaType = null, int highestValue = 9);
        string addNewItem(string name = null, string type = null, string areaType = null, int highestValue = 9);
        string outputData();

        string look();

        List<IItem> getItems();
        List<IGameCreature> getCreatures();
    }
}
