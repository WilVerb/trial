using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace ConsoleAppGenereVoice
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            Console.WriteLine("Type QQQ to quit");

            PromptBuilder pb ;
           
        
            string thePhrase;
            thePhrase = Console.ReadLine();
            while (thePhrase != "QQQ")
            {
                pb = new PromptBuilder(System.Globalization.CultureInfo.CurrentCulture);
                pb.StartStyle(new PromptStyle(PromptRate.Fast));
                pb.StartSentence();
                pb.AppendText(thePhrase);
                pb.EndSentence();
                pb.AppendBreak(PromptBreak.Large);
                pb.StartSentence();
                pb.AppendText("Was it good?",PromptEmphasis.Strong);
                pb.EndSentence();
                pb.EndStyle();
                //ss.Speak(thePhrase);
                ss.Speak(pb);
                thePhrase = Console.ReadLine();
                
            } 
            //Console.ReadLine();
        }
    }
}
