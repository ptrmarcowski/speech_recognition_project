using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog
{
    class Audio : Dialog
    {
        String digitsGram;
        public Audio()
        {
            digitsGram = System.IO.File.ReadAllText("Audio.txt");
        }
        public override string ToString()
        {
            return "Audio";
        }

        public override void Run(Action<string> print)
        {
            // json dynamic object example  :
            //dynamic stuff = JsonConvert.DeserializeObject("{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }");
            //
            //string name = stuff.Name;
            //string address = stuff.Address.City;

            while (true)
            {
                activeSession = new SarmataSession(host, new Dictionary<string, string>{
                        { "grammar", digitsGram },
                        { "complete-timeout", "1000" },
                        { "incomplete-timeout", "3000" },
                        { "no-input-timeout", "7000" },
                        { "no-rec-timeout", "10000" },
                    });

                var response = activeSession.WaitForResponse();
                print(status2string(response));
                response = activeSession.WaitForResponse();
                print(status2string(response));
                dynamic command = JsonConvert.DeserializeObject(
                    response.Results[0].SemanticInterpretation);

                if (command==1)
                {
                    print("Włączone!");
                }
                if (command==2)
                {
                    print("Wyłączone!");
                }
                if(command==3)
                {
                    print("Przełączono!");
                }
                if(command==4)
                {
                    print("Nagrywam, powiedz kiedy skończyć...");
                }
                if(command==5)
                {
                    print("Nagrane!");
                }
                if(command==6)
                {
                    print("Jest godzina 10:23");
                }
                if (command==7)
                {
                    print("Dzisiaj mamy poniedziałek, 30.01.2017");
                }
                if (command==8)
                {
                    print("Tak dobrze?");
                }
                if (command==9)
                {
                    print("Cieszę się!");
                }
                if (command==10)
                {
                    print("Działam, działam! Lepiej teraz?");
                }
                if (command == 11)
                {
                    print("Który numer?");
                }
                if (command == 12)
                {
                    print("Już się robi!");
                }
            }
        }
    }
}
