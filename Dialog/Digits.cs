using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog
{
    class Digits:  Dialog
    {
        String digitsGram;
        public Digits()
        {
            digitsGram = System.IO.File.ReadAllText("command.txt");
        }
        public override string ToString()
        {
            return "digits";
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
            }
        }
    }
}
