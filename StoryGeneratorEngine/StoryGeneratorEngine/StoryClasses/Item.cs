using StoryGeneratorEngine.EngineClasses;
using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.GameClasses
{
    class Item :IItem
    {
        private string name;
        private List<string> types = new List<string>();
        private int amount = 1;
        private int rarity;
        private string description;
        public void addType(string t)
        {
            types.Add(t);
        }

        public List<string> getTypes()
        {
            return types;
        }

        public bool hasType(string type)
        {
            return types.Contains(type.ToLower());   
        }
        public void increaseAmount(int a)
        {
            amount += a;
        }

        public void decreaseAmount(int a)
        {
            amount -= a;
        }

        public int getAmount()
        {
            return amount;
        }


        public void setName(string n)
        {
            name = n;
        }

        public string getName()
        {
            return name;
        }

        public void setRarity(int r)
        {
            rarity = r;
        }

        public int getRarity()
        {
            return rarity;
        }


        public string outputData()
        {
            string output = "It is a " + name + "\n";
            output += description + "\n";
            if(rarity <= 3)
            {
                output += "It is quite common" + "\n";
            }
            else if (rarity <= 6)
            {
                output += "It is uncommon" + "\n";
            }
            else if (rarity <= 9)
            {
                output += "It is quite rare" + "\n";
            }
            else 
            {
                output += "It is very rare" + "\n";
            }
            foreach(string s in types)
            {
                output += s + "\n";
            }

                return output;
        }


        public void setDesc(string desc)
        {
            description = desc;
        }

        public string Use(IGameCreature target)
        {
            foreach (string type in types)
            {
                if (type.ToLower() == "projectile")
                {
                    foreach (string type2 in types)
                    {
                        if (type2.ToLower() == "weapon")
                        {
                            return target.updateHealth(-rarity);      
                        }
                        if( type2.ToLower() == "heal")
                        {
                            return target.updateHealth(rarity);      
                        }
                    }
 
                }

                if (type.ToLower() == "trade")
                {
                    foreach (string type2 in types)
                    {
                        if (type2.ToLower() == "trinket")
                        {
                            return "Traded " + name + " for " + rarity + " gold\n";
                        }
                    }                   
                }
            }
            return "";
        }

        public string Use(IPlayerCharacter player)
        {
            string output = "";
            foreach (string type in types)
            {
                
                        if (type.ToLower() == "weapon")
                        {

                            return player.equip(this);
                        }
                        if (type.ToLower() == "heal")
                        {
                            return player.updateHealth(rarity);
                        }
                        if (type.ToLower() == "food")
                        {
                            output = "You have eaten the " + name + "\n";
                            return output + player.updateHealth(rarity);
                        }
                        if (type.ToLower() == "container")
                        {
                            int amount = StoryGenerationClass.Instance.getRandomNumber(rarity +1);
                            // open container
                            for (int j = 0; j < amount; j++)
                            {
                                
                                output += player.addNewItem(null, null, null, rarity) + "\n";
                                
                            }
                            return "You have opened " + name + "\nYou find:\n" + output; 
                        }
            }
            return "";
        }

        public string Use(ILocation loc,IPlayerCharacter player)
        {
            string output = "";
            int amount = StoryGenerationClass.Instance.getRandomNumber(rarity);
            foreach (string type in types)
            {

                if (type.ToLower() == "container")
                {
                    // open container
                    for (int j = 0; j < amount; j++)
                    {
                      
                       output += loc.addNewItem(null, null, null, rarity) + "\n";
                    }
                    return "You have opened " + name + "\nYou find:\n" + output; 

                   
                }
                if (type.ToLower() == "heal")
                {
                    return player.updateHealth(rarity);
                }
               
            }
            return "";
        }
    }
}
