using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LearnIt.Data.Services
{
    public class JsonParserService : IJsonParserService
    {

        public Course Execute(byte[] fileStream)
        {
            using (StreamReader reader = new StreamReader(new MemoryStream(fileStream)))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    JObject jasonObject = (JObject)JToken.ReadFrom(jsonReader);

                    Course courseModel = new Course();
                    
                    
                    var info = jasonObject.SelectToken("Info");
                    foreach (var entry in info)
                    {
                        var course = entry.SelectToken("Course");
                        courseModel.Name = course.SelectToken("Name").ToString();
                        courseModel.DateAdded = DateTime.Now;
                        courseModel.Description = course.SelectToken("Description").ToString();
                        courseModel.Required = bool.Parse(course.SelectToken("Required").ToString());
                        courseModel.ScoreToPass = int.Parse(course.SelectToken("ScoreToPass").ToString());

                        var questions = course.SelectToken("Questions");
                        foreach (var enter in questions)
                        {
                            Question qstnModel = new Question();
                            qstnModel.Qstn = enter.SelectToken("Question").ToString();
                            qstnModel.Answers = enter.SelectToken("Answers").ToString();
                            qstnModel.RightAnswer = enter.SelectToken("RightAnswer").ToString();
                            courseModel.Questions.Add(qstnModel);
                        }
                        var images = course.SelectToken("Images");
                        int position = 0;
                        foreach (var enter in images)
                        {
                            Image image = new Image();
                            position++;
                            image.ImageBinary = Convert.FromBase64String(enter.ToString());
                            image.Position = position;
                            courseModel.Images.Add(image);
                        }

                        return courseModel;
                    }
                }
            }
            return null;
        }
    }
}
