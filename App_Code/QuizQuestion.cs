using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class QuizQuestion
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public string OptionC { get; set; }
    public string OptionD { get; set; }
    public string OptionE { get; set; }
    public string OptionF { get; set; }
    public string CorrectOption { get; set; }
    public string UserAnswer { get; set; } // User ke answer ko store karne ke liye
}