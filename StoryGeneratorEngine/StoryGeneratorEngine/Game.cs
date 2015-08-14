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
    class Game : IGame
    {
        private volatile bool bExit = false;
        private GameForm gForm;

        private volatile string input = "test";

        private string lastInMessage;
        private bool bHasNewMessage;

        // game variables
        private List<ILocation> locations = new List<ILocation>();
        private IPlayerCharacter player;
        private ILocation currentLocation;
        private IStory Story;

        private int x, y; // current location

        public Game()
        {
            Story = StoryGenerationClass.Instance.generateStory();
            Init();
        }
        public Game(Story newstory)
        {
            Story = newstory;
            Init();
        }

        public void Init()
        {
            //do stuff
            gForm = new GameForm();
            // generate world and everything in it    
            GenerateWorld();
            //populate world
            //set end condition

            //look();
            clearOutputText();
            // show plot
            AddOutputMessage(Story.getStory());
            AddOutputMessage("This where you begin your adventure...");
        }

        public void RunGame()
        {
            
            // let the games begin
            Application.Idle += Tick;
            Application.Run(gForm);
        }

        public void Tick(object sender, EventArgs e)
        {
            Thread.Sleep(16); // 60 fps
            if (bHasNewMessage)
            {
                Tick();


                bool bBeenAttacked = false;
                // check enemies state
                foreach(IGameCreature gc in currentLocation.getCreatures())
                {
                    // check if enemy is dead
                    if(gc.getHealth() <= 0)
                    {
                        player.updateScore(gc.getCreature().getValue());
                        if(gc.getCreature().getValue() < 10)
                        {
                            currentLocation.addNewItem(null, null, null, gc.getCreature().getValue());
                        }
                        currentLocation.getCreatures().Remove(gc);
                        AddOutputMessage("You have defeated " + gc.getCreature().getName());
                        if(Story.getPlot().getModifier().ToLower() == "creature" && gc.getCreature().getName() == Story.getTargetName())
                        {
                            player.updateScore(100);
                            EndGame("You have won, congratulations.\nYour Score: " + player.getScore());
                        }
                        break;
                    }
                    //attack
                    if(gc.getIsHostile())
                    {
                        bBeenAttacked = true;
                        AddOutputMessage(gc.attack(player));
                    }
                }
                if (Story.getPlot().getModifier().ToLower() == "item" && !bBeenAttacked)
                {
                        foreach(IItem i in player.getInv())
                        {
                            if(i.getName().ToLower() == Story.getTargetName().ToLower())
                            {
                                player.updateScore(100);
                                EndGame("You have won, congratulations.\nYour Score: " + player.getScore());
                            }
                         }
                }
                //check end game
                if(player.getHealth() <= 0)
                {
                    EndGame("You have died, you lose.\nYour Score: " + player.getScore());
                }

                
            }
            
        }
        public void Tick()
        {
           
           
                //do what input said
                // player interaction
                if (lastInMessage.StartsWith("quit") || lastInMessage.StartsWith("exit"))
                {
                    Program.End();

                }
                //else if (lastInMessage.StartsWith("new "))
                //{
                //    lastInMessage = lastInMessage.Replace("new ", "");

                //    if (lastInMessage.StartsWith("story"))
                //    {
                //        Story = StoryGenerationClass.Instance.generateStory(1,1,3);
                //        GenerateWorld();
                //        OutputStory();
                //    }
                //    else
                //    {
                //        AddOutputMessage("cannot create new : " + lastInMessage);
                //    }
                   
                //}
                else if (lastInMessage.StartsWith("attack "))
                {
                    lastInMessage = lastInMessage.Replace("attack ", "");
                    if (lastInMessage != "")
                    {
                        foreach (IGameCreature c in currentLocation.getCreatures())
                        {
                            if (c.getCreature().getName().ToLower().Contains(lastInMessage))
                            {
                                look();
                                AddOutputMessage(player.attack(c));
                                bHasNewMessage = false;
                                return;
                            }
                        }
                    }
                    AddOutputMessage("Cannot attack " + lastInMessage + "\n");
                }
                else if (lastInMessage.StartsWith("look"))
                {
                    if (lastInMessage.Equals("look"))
                    {
                        look();
                        bHasNewMessage = false;
                        return;
                    }
                    lastInMessage = lastInMessage.Replace("look ", "");

                    if (lastInMessage != "")
                    {
                        if (lastInMessage.StartsWith("self"))
                        {
                            look(player);
                            bHasNewMessage = false;
                            return;
                        }
                        if (lastInMessage.StartsWith("story"))
                        {
                            OutputStory();
                            bHasNewMessage = false;
                            return;
                        }
                        foreach (IGameCreature c in currentLocation.getCreatures())
                        {
                            if (c.getCreature().getName().ToLower().Contains(lastInMessage))
                            {
                                look(c);
                                bHasNewMessage = false;
                                return;
                            }
                        }
                        foreach (IItem i in currentLocation.getItems())
                        {
                            if (i.getName().ToLower().Contains(lastInMessage))
                            {
                                look(i);
                                bHasNewMessage = false;
                                return;
                            }
                        }
                        if (player.getInv().Count != 0)
                        {
                            foreach (IItem i in player.getInv())
                            {
                                if (i.getName().ToLower().Contains(lastInMessage))
                                {
                                    look(i);
                                    bHasNewMessage = false;
                                    return;
                                }
                            }
                        }
                    }
                    AddOutputMessage("Cannot look at " + lastInMessage + "\n");


                }
                else if (lastInMessage.StartsWith("take "))
                {
                    lastInMessage = lastInMessage.Replace("take ", "");
                    if (lastInMessage != "")
                    {
                        Take(lastInMessage);
                    }


                }
                else if (lastInMessage.StartsWith("use "))
                {
                    lastInMessage = lastInMessage.Replace("use ", "");
                    IGameCreature target = null;
                    if (lastInMessage != "")
                    {
                        // split message
                        string[] parts = lastInMessage.Split(' ');
                        if (parts.Length > 1)
                        {
                            // check targets
                            foreach (IGameCreature c in currentLocation.getCreatures())
                            {
                                if (c.getCreature().getName().ToLower().StartsWith(parts[1]))
                                {
                                    target = c;
                                }
                            }
                            if (target == null)
                            {
                                look();
                                AddOutputMessage("Cannot find " + parts[1]);
                                bHasNewMessage = false;
                                return;
                            }
                        }


                        foreach (IItem inv in player.getInv())
                        {
                            if (inv.getName().ToLower().Contains(parts[0]))
                            {
                                if (target != null)
                                {
                                    string output = inv.Use(target);
                                    if (output != "")
                                    {
                                        look();
                                        AddOutputMessage("Used " + parts[0]);
                                        AddOutputMessage(output);
                                        player.getInv().Remove(inv);
                                    }
                                    else
                                    {
                                        look();
                                        if (target != null) AddOutputMessage("Could not use " + parts[0] + " with " + parts[1]);
                                        else AddOutputMessage("Could not use " + parts[0]);

                                    }
                                }
                                else
                                {
                                    string output = inv.Use(player);
                                    if (output != "")
                                    {
                                        AddOutputMessage("Used " + parts[0]);
                                        AddOutputMessage(output);
                                        player.getInv().Remove(inv);
                                    }
                                    else
                                    {
                                        AddOutputMessage("Could not use " + parts[0]);
                                    }
                                }
                                bHasNewMessage = false;
                                return;
                            }
                        }
                        foreach (IItem i in currentLocation.getItems())
                        {
                            if (i.getName().ToLower().Contains(parts[0]))
                            {
                                if (target != null)
                                {
                                    string output = i.Use(target);
                                    if (output != "")
                                    {
                                        look();
                                        AddOutputMessage("Used " + parts[0]);
                                        AddOutputMessage(output);
                                        currentLocation.getItems().Remove(i);
                                    }
                                    else
                                    {

                                        look();
                                        if (target != null) AddOutputMessage("Could not use " + parts[0] + " with " + parts[1]);
                                        else AddOutputMessage("Could not use " + parts[0]);

                                    }
                                }
                                else
                                {
                                    string output = i.Use(currentLocation, player);
                                    if (output != "")
                                    {
                                        look();
                                        AddOutputMessage("Used " + parts[0]);
                                        AddOutputMessage(output);
                                        currentLocation.getItems().Remove(i);
                                    }
                                    else
                                    {
                                        look();
                                        AddOutputMessage("Could not use " + parts[0] + ", perhaps pick it up first");
                                    }
                                }
                                bHasNewMessage = false;
                                return;
                            }
                        }
                        look();
                        AddOutputMessage("Cannot find " + parts[0]);
                        bHasNewMessage = false;
                        return;
                    }


                }
                else if ("inventory".StartsWith(lastInMessage))
                {
                    look();
                    AddOutputMessage(player.getItemNames());
                }
                else if ("up".StartsWith(lastInMessage) || "north".StartsWith(lastInMessage))
                {
                    move(0, -1);
                }
                else if ("down".StartsWith(lastInMessage) || "south".StartsWith(lastInMessage))
                {
                    move(0, 1);

                }
                else if ("left".StartsWith(lastInMessage) || "west".StartsWith(lastInMessage))
                {
                    move(-1, 0);
                }
                else if ("right".StartsWith(lastInMessage) || "east".StartsWith(lastInMessage))
                {
                    move(1, 0);
                }
                else
                {
                    look();
                    AddOutputMessage("unknown command : " + lastInMessage);
                } 
              
                bHasNewMessage = false;

        }

        private void GenerateWorld()
        {
            int currentX = 0, currentY = 0;
            List<int> xPositions = new List<int>(), yPositions = new List<int>();
            // create locations
            foreach(IAreaType aT in this.Story.getAreas())
            {
                List<ILocation> newLocs = StoryGenerationClass.Instance.getLocation(aT.getName(), aT.getValue());
                foreach (ILocation l in newLocs)
                {
                    l.setLocation(currentX, currentY);
                    locations.Add(l);
                    xPositions.Add(currentX);
                    yPositions.Add(currentY);
                    
                    bool bNotFound = true, bLocationAvailable = false;
                    int lastPosition = xPositions.Count -1;
                    while(!bLocationAvailable)
                    {
                         // get new location
                         int direction = StoryGenerationClass.Instance.getRandomNumber(4);
                   
                          if(direction == 0) // up
                          {
                             bNotFound = true;
                           for(int i = 0; i < xPositions.Count;i++)
                           {
                             if(xPositions[i] == currentX && yPositions[i] == currentY +1)
                             {
                                bNotFound = false;
                                direction++;
                             }
                            
                           }
                           if (bNotFound)
                           {
                               currentY += 1;
                               bLocationAvailable = true;
                           }

                          }
                          if (direction == 1) // down
                          {
                              bNotFound = true;
                              for (int i = 0; i < xPositions.Count; i++)
                              {
                                  if (xPositions[i] == currentX && yPositions[i] == currentY - 1)
                                  {
                                      bNotFound = false;
                                      direction++;
                                  }

                              }
                              if (bNotFound)
                              {
                                  currentY--;
                                  bLocationAvailable = true;
                              }


                          }
                        if (direction == 2) // right
                        {
                           bNotFound = true;
                           for(int i = 0; i < xPositions.Count;i++)
                           {
                             if(xPositions[i] == currentX +1 && yPositions[i] == currentY)
                             {
                                bNotFound = false;
                                direction++;
                             }
                            
                           }
                           if (bNotFound)
                           {
                               currentX += 1;
                               bLocationAvailable = true;
                           }

                        }
                       
                        if (direction ==3) // left
                        {
                            bNotFound = true;
                            for(int i = 0; i < xPositions.Count;i++)
                           {
                             if(xPositions[i] == currentX -1  && yPositions[i] == currentY)
                             {
                                bNotFound = false;
                                direction++;
                             }
                            
                           }
                             if (bNotFound)
                             {
                                 currentX--;
                                 bLocationAvailable = true;
                             }
      
                        }
                        if(direction > 3) // no space available
                        {
                            lastPosition--;
                            if (lastPosition == 0)
                            {
                                bLocationAvailable = true;
                            }
                            currentX = xPositions[lastPosition];
                            currentY = yPositions[lastPosition];
                        }
                        
                    }
                    
                }
            }

            // add target item to last position
            if(Story.getPlot().getModifier().ToLower() == "creature")
            {
               locations[locations.Count-1].addNewCreature(Story.getTargetName(),null,null,10);

            }
            else if (Story.getPlot().getModifier().ToLower() == "item")
            {
                locations[locations.Count-1].addNewItem(Story.getTargetName(), null, null, 10);
            }


            // set positions
            x = 0; y = 0;
            // set player start
            currentLocation = locations[0];
            player = new PlayerCharacter(Story.getMainCharacters()[0]);
            // update map

            updateMap();
           

        }


        public void updateMap()
        {
            gForm.clearMap();
            foreach (ILocation l in locations)
            {

                gForm.updateMap(l.getX() - x, l.getY() - y, l.getArea().getType().getColour());
            }
        }
        public void doEvent()
        {
            for (int i = 0; i < currentLocation.getEvent().getMods().Count; i++)
            {
                if(currentLocation.getEvent().getMods()[i].ToLower() == "creature")
                {
                    AddOutputMessage("Creatures appeared:");
                    // spawn creature
                    for (int j = 0; j < currentLocation.getEvent().getValues()[i]; j++)
                    {
                        AddOutputMessage(currentLocation.addNewCreature(currentLocation.getEvent().getTypes()[i], currentLocation.getEvent().getTypes()[i], currentLocation.getEvent().getAreaType()));
                    }
                }
                if (currentLocation.getEvent().getMods()[i].ToLower() == "item")
                {
                    //spawn item
                    AddOutputMessage(currentLocation.addNewItem(currentLocation.getEvent().getTypes()[i], currentLocation.getEvent().getTypes()[i], currentLocation.getEvent().getAreaType(), currentLocation.getEvent().getValues()[i]) + " has appeared");
             
                }
                if (currentLocation.getEvent().getMods()[i].ToLower() == "player")
                {
                    AddOutputMessage("Player Stuff needs to be done");
                }

            }

            Story.addToCurrentEvents("In the " + currentLocation.getArea().getName() + " " + currentLocation.getEvent().outputData());
            AddOutputMessage("\n" + currentLocation.getEvent().outputData());


        }


        public void use(IItem item)
        {
            //check what type it is

            // do action on what type it is


        }

        public void Take(string item)
        {
           //add item to inventory
            foreach(IItem i in currentLocation.getItems())
            {
                if(i.getName().ToLower().Contains(item))
                {
                    if(i.hasType("immobile"))
                    {
                        
                        look();
                        AddOutputMessage("Cannot pick up " + i.getName() + " it is immobile.\n");
                        return;
                    }
                    else
                    {
                        player.addItem(i);
                        player.updateScore(i.getRarity());
                        currentLocation.getItems().Remove(i);
                        look();
                        AddOutputMessage("You have picked up " + i.getName() + ".\n");                      
                        return;
                    }
                }

            }
            AddOutputMessage("Cannot find " + item + ".\n");
           



        }

        public void look(IItem item)
        {
            // output items name, types ,desc
            AddOutputMessage("From what you can tell of the item:");
            AddOutputMessage(item.outputData());
        }
        public void look(IPlayerCharacter c)
        {
            // output items name, types ,desc
            clearOutputText();
            AddOutputMessage(c.outputData());
        }
        public void look(IGameCreature c)
        {
            // output crateures stats, desc
            AddOutputMessage("From what you can tell from the creature:");
            AddOutputMessage(c.outputData());
        }
        public void look()
        {
            // output current locations stats , items creatures , description
            clearOutputText();
            AddOutputMessage(currentLocation.look());
        }
        public void move(int y, int x)
        {
            foreach (IGameCreature gc in currentLocation.getCreatures())
            {
                //attack
                if (gc.getIsHostile())
                {
                    AddOutputMessage(gc.getCreature().getName() + " is currently stopping you from escaping");
                    return;
                }
            }
            foreach (ILocation l in locations)
            {
                if(l.getX() == this.x + x && l.getY() == this.y + y)
                {
                    this.x += x;
                    this.y += y;
                    currentLocation = l;
                    updateMap();
                    look();
                    if (!l.hasDoneEvent())
                    {
                        doEvent();
                        l.setDoneEvent();
                    }
                    return;
                }
            }
            AddOutputMessage("You cannot go in that direction");
           
        }
        public void clearOutputText()
        {
            gForm.ClearText();
        }
        public void OutputStory()
        {
            clearOutputText();
            AddOutputMessage(Story.getStory());
            AddOutputMessage(Story.getCurrentEvents());
        }


        public void EndGame(string message)
        {
            // message box with score
            AddOutputMessage("END");
            MessageBox.Show(message);
            bExit = true;
            Program.End();
        }
       
        public void ChangeInputMessage(string s)
        {
            lastInMessage = s;
            bHasNewMessage = true;
        }
        public void AddOutputMessage(string s)
        {
            gForm.AddLine(s);
        }
    }
}
