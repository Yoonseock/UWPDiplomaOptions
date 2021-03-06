﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace UWPDiplomaOptions.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }
        public string StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string FirstChoiceOptionTitle { get; set; }
        public string SecondChoiceOptionTitle { get; set; }
        public string ThirdChoiceOptionTitle { get; set; }
        public string FourthChoiceOptionTitle { get; set; }
        public string SelectionDate { get; set; }
        public int Year { get; set; }
        public int Term  { get; set; }
    }

    public class ChoiceManager
    {
        public static async Task GetChoices(ObservableCollection<Choice> ChoiceList)
        {
            var http = new HttpClient();
            var response = await http.GetAsync("http://uwproject.feifei.ca/api/Choices");
            var result = await response.Content.ReadAsStringAsync();

            JsonValue value = JsonValue.Parse(result);
            JsonArray root = value.GetArray();
            for (uint i = 0; i < root.Count; i++)
            {
                var obj = root.GetObjectAt(i);
                int choiceId = (int)obj.GetNamedNumber("ChoiceId");
                string studentId = obj.GetNamedString("StudentId");
                string studentFirstName = obj.GetNamedString("StudentFirstName");
                string studentLastName = obj.GetNamedString("StudentLastName");
                string firstChoiceOptionTitle = obj.GetNamedString("FirstChoiceOptionTitle");
                string secondChoiceOptionTitle = obj.GetNamedString("SecondChoiceOptionTitle");
                string thirdChoiceOptionTitle = obj.GetNamedString("ThirdChoiceOptionTitle");
                string fourthChoiceOptionTitle = obj.GetNamedString("FourthChoiceOptionTitle");
                string selectionDate = obj.GetNamedString("SelectionDate");
                int year = (int)obj.GetNamedNumber("Year");
                int term = (int)obj.GetNamedNumber("Term");
                var choice = new Choice
                {
                    ChoiceId = choiceId,
                    StudentId = studentId,
                    StudentFirstName = studentFirstName,
                    StudentLastName = studentLastName,
                    FirstChoiceOptionTitle = firstChoiceOptionTitle,
                    SecondChoiceOptionTitle = secondChoiceOptionTitle,
                    ThirdChoiceOptionTitle = thirdChoiceOptionTitle,
                    FourthChoiceOptionTitle = fourthChoiceOptionTitle,
                    SelectionDate = selectionDate,
                    Year = year,
                    Term = term
                };
                ChoiceList.Add(choice);
            }
        }
    }
}
