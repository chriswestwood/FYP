using StoryGeneratorEngine.GameClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.EngineClasses
{
    static class LoadFromFile
    {
       public static string[] LoadFileToString(String filename)
        {
           List<string> data = new List<string>();
           CreateDefaultFile(filename);
            try
            {
                    string[] dataString = System.IO.File.ReadAllLines(filename);
                    for (int i = 0; i < dataString.Length; i++)
                    {
                        data.Add(dataString[i]);
                    }
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data[j].StartsWith(";"))
                        {
                            data.Remove(data[j]);
                            j--;

                        }
                    }
                
             }
            catch
            {    
               data.Add("No file exists with that filename , creating file");
                // create txt file
               System.IO.File.WriteAllText(filename, "");

            }
           return data.ToArray();
        }
       //public static Story LoadStoryFromFile(String filename)
       //{
           //Story FileStory = new Story();
           // Create story from file specified
       //    return FileStory;
       //}

        public static void CreateDefaultFile(String filename)
       {
            // create the default textfiles for use if they dont exist

       }
    }
}
