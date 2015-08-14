using StoryGeneratorEngine.GameClasses;
using StoryGeneratorEngine.Interfaces;
using StoryGeneratorEngine.StoryClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StoryGeneratorEngine.EngineClasses
{
    class StoryGenerationClass
    {

       private static StoryGenerationClass instance;

        //storage of file data
        // character data
       private static string[] firstNames;
       private static string[] lastNames;
       private static IRole[] roles;
       private static string[] races;
        // setting data
       private static IArea[] locations;
       private static string[] locationNames;
       private static IAreaType[] locationAreaTypes;
        // plot data
       private static IPlot[] plotTypes;
        // event data
       private static IEvent[] events;
       private static ICreature[] creatures;
       private static IItem[] items;

        // basic number generation
       private Random randomNumberGen = new Random();


       private StoryGenerationClass() 
       {
           LoadData();
       }
        // create instance of itself when called if one not available 
     public static StoryGenerationClass Instance
     {
      get 
      {
         if (instance == null)
         {
             instance = new StoryGenerationClass();

         }
         return instance;
      }
     }
        // load all data in from files
        private void LoadData()
        {
            // load character data
            firstNames = LoadFromFile.LoadFileToString("FirstNames.txt");
            lastNames = LoadFromFile.LoadFileToString("LastNames.txt");
            ConvertToRoles(LoadFromFile.LoadFileToString("Roles.txt"));
            races = LoadFromFile.LoadFileToString("Races.txt");
            //load setting data
            ConvertToAreaTypes(LoadFromFile.LoadFileToString("LocationTypes.txt"));
            ConvertToAreas(LoadFromFile.LoadFileToString("Locations.txt"));
           
           locationNames =  LoadFromFile.LoadFileToString("LocationNames.txt");
            // load plot data
            ConvertToPlots(LoadFromFile.LoadFileToString("Plots.txt"));
            // load event Data
            ConvertToItems(LoadFromFile.LoadFileToString("Items.txt"));
            ConvertToCreatures(LoadFromFile.LoadFileToString("Creatures.txt"));
            ConvertToEvents(LoadFromFile.LoadFileToString("Events.txt"));
        }

        // generate a completely new story
        public IStory generateStory(int numMainCharacters = 1, int numOfTargetCharacters = 1, int numOfAreas = 0)
        {
            // mkae sure there is data to use
            LoadData();
            // pick a plot to use
            IStory newStory = new Story(getPlot());
            Random randomNumber = new Random();

            // create a new random story

            // create main characters
            for (int i = 0; i < numMainCharacters; i++)
            {   
               newStory.addMainCharacter(getCharacter()); // get random character
            }
            for (int i = 0; i < numOfTargetCharacters; i++)
            {
                newStory.addEnemyCharacter(getCharacter()); // get random character
            }

            // find how many areas to create
            if(numOfAreas == 0)
            {
                numOfAreas = getRandomNumber(3) + 3;
            }
            // create x amonut of areas
            for (int i = 0; i < numOfAreas; i++)
            {
                newStory.addAreaType(getAreaType()); // create areas
            }

            //  update plot based upon goal

            return newStory;


        }
        // save story
        // load story
        // modify story


       


        public IPlot getPlot(int number = 0)
        {
           
            if(number != 0)
            {
                // get plot
                return plotTypes[number - 1];
            }
            else
            {
                // get random plot
                return plotTypes[getRandomNumber(plotTypes.Length)];
            }
        }

        public IPlot[] getAllPlot() { return plotTypes; }


        public ICharacter getCharacter( string inFullname = null , char ingender = ' ' , int inAge = 0, string inRole = null , string inRace = null)
        {

            string fullname;
            char gender = 'N';
            int age;
            IRole role = null;
            string race;
            // create name
            if (inFullname != null) fullname = inFullname;
            else
            {
                fullname = firstNames[getRandomNumber(firstNames.Length)];
                gender = fullname.Split(':')[0].ToUpper().ToCharArray()[0];
                fullname = fullname.Split(':')[1];
                //split fullname into gender and firstname
                fullname += " " + lastNames[getRandomNumber(lastNames.Length)];
            }
            // give gender
            if (ingender != ' ') gender = ingender;
            // create age
            if (inAge != 0) age = inAge;
            else age = getRandomNumber(20) + 18;
            // give role
            if(inRole != null)
            {
                // search roles for string

                //role = roles[getRandomNumber(roles.Length)];
            }
            else // find random role
            {
                role = roles[getRandomNumber(roles.Length)];
            }
            // give Race
            if (inRace != null) race = inRace;
            else race = races[getRandomNumber(races.Length)];
            // create character
            ICharacter newChar = new Character(fullname,gender,age,role , race);
            // return
            return newChar;
        }


        public string[] getAllFirstNames() { return firstNames; }
        public string[] getAllLastNames() { return lastNames; }
        public IRole[] getAllRoles() { return roles; }
        public string[] getAllRaces() { return races; }

        public IEvent GetEvent(string areaType = null)
        {

            if (areaType != null)
            {
                List<IEvent> typeEvents = new List<IEvent>();
                // find events with type
                
                    foreach (IEvent e in events)
                    {
                        if (e.getAreaType() == areaType)
                        {
                            typeEvents.Add(e);
                        }
                    }
                if (typeEvents.Count != 0)
                {

                    return typeEvents[getRandomNumber(typeEvents.Count)];
                }
            }
            
                // rturn random event
                return events[getRandomNumber(events.Length)];
           
           
        }
        public string getLocationName(string type)
        {
            string name = locationNames[getRandomNumber(locationNames.Length)];
            if(name.StartsWith("of"))
            {
                return type + " " + name;
            }
            return name + " " + type;

        }


        //return an location with an area and a name based upon requirements ( a particular type of area , or if a max size is specified)
        public List<ILocation> getLocation(string type = null , int maxSize = 1)
        {
            List<ILocation> newLocation = new List<ILocation>();
            // get random name
            string name = getLocationName(type);
            // search
            int totalSize = maxSize / 2;
            totalSize += getRandomNumber(totalSize) + 1;
            for (int i = 0; i < totalSize; i++)
            {
                ILocation l = new Location(type);
                l.setName(name);
                // set event
                l.setEvent(GetEvent(type));
                int max = getRandomNumber(3) + 1;
                for (int j = 0; j < max; j++) l.addItem(getItem(null, null, type));
                max = getRandomNumber(3);
                for (int j = 0; j < max; j++) l.addCreature(getCreature(null, null, type));
                    newLocation.Add(l);
            }
                // give name
                return newLocation;

        }
        public IAreaType getAreaType(string type = null)
        {

            

             // find area type if given
            if (type != null)
            {
                foreach (IAreaType at in locationAreaTypes)
                {
                    if (at.getName().ToLower() == type.ToLower())
                    {
                        return at;
                    }
                }
            }
            // else return random area type
            return locationAreaTypes[getRandomNumber(locationAreaTypes.Length)];

        }

        public IArea getArea(string type = null)
        {
                // find all area with type
                List<IArea> pontentialAreas = new List<IArea>();
                foreach(IArea a in getAllAreas())
                {
                    if(a.getType().getName().ToLower() == type.ToLower() || type.ToLower() == "any")
                    {
                        pontentialAreas.Add(a);
                    }
                }
                if (pontentialAreas.Count != 0) return pontentialAreas[getRandomNumber(pontentialAreas.Count)];
                
                 return locations[getRandomNumber(locations.Length)];
           
        }

        public IArea[] getAllAreas() { return locations; }
        public string[] getAllAreaNames() { return locationNames; }
        


        // return an item that matches the requirements ( item name , a particular type , a location it can spawn in, or the highest value it can have)
        // no requirements will return a random item
        public IItem getItem(string name = null,string type = null, string areaType = null , int highestValue = 9)
        {

            // serach if it can find named item
            if (name != null)
            {
                foreach (IItem i in items)
                {
                    if (i.getName().ToLower() == name.ToLower())
                    {
                        return i;
                    }
                }
            }
            int randomValue;
            if(highestValue == 10)
            {
                randomValue = 10;
            }
            else
            {
                randomValue = getRandomNumber(highestValue) + 1;
            }
            List<IItem> valuedItems = new List<IItem>();
            foreach (IItem i in items)
            {
                if (randomValue == 10)
                {
                    if (i.getRarity() == randomValue)
                    {
                        valuedItems.Add(i);
                    }
                }
                else
                {
                    if (i.getRarity() <= randomValue)
                    {
                        valuedItems.Add(i);
                    }
                }


            }
            List<IItem> typedItems = new List<IItem>();
            //  if under value
            foreach (IItem i in valuedItems)
            {
                if (type != null && i.hasType(type))
               {             
                      typedItems.Add(i);

               }
               else if (areaType != null && i.hasType(areaType))
               {
                       typedItems.Add(i);
               }

            }
            if(typedItems.Count == 0)
            {
                return valuedItems[getRandomNumber(valuedItems.Count)];
            }
            else
            {
                return typedItems[getRandomNumber(typedItems.Count)];
            }
        }

        public IItem[] getAllItems() { return items; }



        // return a creature that matches the requirements ( creature name , a particular type , a location it can spawn in, or the highest value it can have)
        // no requirements will return a random creature
        public ICreature getCreature(string name = null , string type = null , string areaType = null , int highestValue = 9)
        {

            // serach if it can find named item
            if (name != null)
            {
                foreach (ICreature c in creatures)
                {
                    if (c.getName().ToLower() == name.ToLower())
                    {
                        return c;
                    }
                }
            }
            int randomValue;
            if (highestValue == 10)
            {
                randomValue = 10;
            }
            else
            {
                randomValue = getRandomNumber(highestValue) + 1;
            }
            List<ICreature> valuedCreatures = new List<ICreature>();
            foreach (ICreature c in creatures)
            {
                if (randomValue == 10)
                {
                    if (c.getValue() == randomValue)
                    {
                        valuedCreatures.Add(c);
                    }
                }
                else
                {
                    if (c.getValue() <= randomValue)
                    {
                        valuedCreatures.Add(c);
                    }
                }


            }
            List<ICreature> typedCreatures = new List<ICreature>();
            //  if under value
            foreach (ICreature c in valuedCreatures)
            {
                if (type != null && c.hasType(type))
                {
                    typedCreatures.Add(c);

                }
                else if (areaType != null && c.hasType(areaType))
                {
                    typedCreatures.Add(c);
                }

            }
            if (typedCreatures.Count == 0)
            {
                return valuedCreatures[getRandomNumber(valuedCreatures.Count)];
            }
            else
            {
                return typedCreatures[getRandomNumber(typedCreatures.Count)];
            }
        }

        public ICreature[] getAllCreatures() { return creatures; }



        // simplified random number generator // CHANGE TO USE SEED
        public int getRandomNumber(int max) { return randomNumberGen.Next(max); }


        // converts array of strings into the relevant data type to be used in stories
        private void ConvertToRoles(string[] inString)
        {
            //Name = name , Type = specialisation( weapon prof , armour usage , other ) eg . sword , lightArmour , Any , none , Desc = description of role
            IRole newRole;
            roles = new Role[inString.Length];
            for(int i =0; i < inString.Length ; i++)
            {
                newRole = new Role();
                // split sthe string
                string[] parts = inString[i].Split(',');
                for (int j = 0; j < parts.Length;j++)
                {
                    string[] bits = parts[j].Split(':');
                    // add data to new role
                    // = name
                    if(bits[0].ToLower().Equals("name"))
                    {
                        newRole.setName(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("type"))
                    {
                        newRole.addType(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("desc"))
                    {
                        newRole.setDesc(bits[1]);
                    }
                }



                    roles[i] = newRole;

            }



           
        }
        private void ConvertToAreas(string[] inString)
        {
          // Name=name of area , Type = locationType it can be used for ,Desc = Description of area
            IArea newArea;
            locations = new Area[inString.Length];
            for (int i = 0; i < inString.Length; i++)
            {
                newArea = new Area();
                // split sthe string
                string[] parts = inString[i].Split(',');
                for (int j = 0; j < parts.Length; j++)
                {
                    string[] bits = parts[j].Split(':');
                    // add data to new role
                    // = name
                    if (bits[0].ToLower().Equals("name"))
                    {
                        newArea.setName(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("type"))
                    {
                        // find areaType
                        newArea.addType(getAreaType(bits[1]));
                    }
                    else if (bits[0].ToLower().Equals("desc"))
                    {
                        newArea.setDesc(bits[1]);
                    }
                }



                locations[i] = newArea;

            }

        }
        private void ConvertToAreaTypes(string[] inString)
        {
            //Name = Name of Area Type , Colour = colour used for map , Value = size used for map
            IAreaType newAreaType;
            locationAreaTypes = new AreaType[inString.Length];
            for (int i = 0; i < inString.Length; i++)
            {
                newAreaType = new AreaType();
                // split sthe string
                string[] parts = inString[i].Split(',');
                for (int j = 0; j < parts.Length; j++)
                {
                    string[] bits = parts[j].Split(':');
                    // add data to new role
                    // = name
                    if (bits[0].ToLower().Equals("name"))
                    {
                        newAreaType.setName(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("colour"))
                    {
                        newAreaType.setColour(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("value"))
                    {
                        newAreaType.setValue(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("desc"))
                    {
                        newAreaType.setDesc(bits[1]);
                    }
                }



                locationAreaTypes[i] = newAreaType;

            }
        }
       
        private void ConvertToItems(string[] inString)
        {
           //Name = name of Item , Type = eg. food , weapon , Value = number used for iteraction ,  Desc = description of item
            IItem newItem;
            items = new Item[inString.Length];
            for (int i = 0; i < inString.Length; i++)
            {
                newItem = new Item();
                // split sthe string
                string[] parts = inString[i].Split(',');
                for (int j = 0; j < parts.Length; j++)
                {
                    string[] bits = parts[j].Split(':');
                    // add data to new role
                    // = name
                    if (bits[0].ToLower().Equals("name"))
                    {
                        newItem.setName(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("type"))
                    {
                        newItem.addType(bits[1].ToLower());
                    }
                    else if (bits[0].ToLower().Equals("value"))
                    {
                        newItem.setRarity(Int32.Parse(bits[1]));
                    }
                    else if (bits[0].ToLower().Equals("desc"))
                    {
                        newItem.setDesc(bits[1]);
                    }
                }



                items[i] = newItem;

            }
        }
        private void ConvertToPlots(string[] inString)
        {
            //Name = name of plot , Mod : what needs to be done eg. kill , find ... , type: creature:item = what needs to be modified , value = by how much
            IPlot newPlot;
            plotTypes = new IPlot[inString.Length];
            for (int i = 0; i < inString.Length; i++)
            {
                newPlot = new Plot();
                // split sthe string
                string[] parts = inString[i].Split(',');
                for (int j = 0; j < parts.Length; j++)
                {
                    string[] bits = parts[j].Split(':');
                    // add data to new role
                    // = name
                    if (bits[0].ToLower().Equals("name"))
                    {
                        newPlot.setName(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("mod"))
                    {
                        newPlot.setModifier(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("type"))
                    {
                        newPlot.setType(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("value"))
                    {
                        newPlot.setValue(Int32.Parse(bits[1]));
                    }
                    else if (bits[0].ToLower().Equals("desc"))
                    {
                        newPlot.setDesc(bits[1]);
                    }
                }



                plotTypes[i] = newPlot;

            }
        }
        private void ConvertToCreatures(string[] inString)
        {
         // Name = type of creature , Type = sub types eg.hostile,unkillable... , Value = value of creature difficulty , Desc = description of creature
            ICreature newCreature;
            creatures = new Creature[inString.Length];
            for (int i = 0; i < inString.Length; i++)
            {
                newCreature = new Creature();
                // split sthe string
                string[] parts = inString[i].Split(',');
                for (int j = 0; j < parts.Length; j++)
                {
                    string[] bits = parts[j].Split(':');
                    // add data to new role
                    // = name
                    if (bits[0].ToLower().Equals("name"))
                    {
                        newCreature.setName(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("type"))
                    {
                        newCreature.addType(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("value"))
                    {
                        newCreature.setValue(Int32.Parse(bits[1]));
                    }
                    else if (bits[0].ToLower().Equals("desc"))
                    {
                        newCreature.setDesc(bits[1]);
                    }
                }



                creatures[i] = newCreature;

            }
        }
        private void ConvertToEvents(string[] inString)
        {
        // Type = areas it can be used in , Mod = modifier eg spawn, damage, heal ( what happends to the object ) , creature:player:item = what is to be modified , Value = amount by , Desc = overall desc of what happend
            IEvent newEvent;
            events = new Event[inString.Length];
            for (int i = 0; i < inString.Length; i++)
            {
                newEvent = new Event();
                // split sthe string
                string[] parts = inString[i].Split(',');
                for (int j = 0; j < parts.Length; j++)
                {
                    string[] bits = parts[j].Split(':');
                    // add data to new role
                    // = name
                    if (bits[0].ToLower().Equals("areatype"))
                    {
                        newEvent.setAreaType(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("mod"))
                    {
                        newEvent.addMod(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("value"))
                    {
                        newEvent.addValue(Int32.Parse(bits[1]));
                    }
                    else if (bits[0].ToLower().Equals("type"))
                    {
                        newEvent.addType(bits[1]);
                    }
                    else if (bits[0].ToLower().Equals("desc"))
                    {
                        newEvent.setDesc(bits[1]);
                    }
                }
                events[i] = newEvent;
            }
        }



    }


}
