using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog
{
    class Calculator : Dialog
    {
        String numberGram;
        String operationGram;
        String commandGram;
        public Calculator()
        {
            //numberGram = System.IO.File.ReadAllText("number.txt");
            //operationGram = System.IO.File.ReadAllText("operation.txt");
            commandGram = System.IO.File.ReadAllText("command.txt");
        }
        public override string ToString()
        {
            return "calculator";
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
                        { "grammar", commandGram },
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
                
                double wynik = 0;
                if (command.operation == "plus")
                {
                    wynik = command.arg1 + command.arg2;
                }
                if (command.operation == "minus")
                {
                    wynik = command.arg1 - command.arg2;
                }
                if (command.operation == "razy")
                {
                    wynik = command.arg1 * command.arg2;
                }
                if (command.operation == "przez")
                {
                    wynik = command.arg1 / (double)command.arg2;
                }
                print("Wynik = " + wynik.ToString());
            }
        }
    }
}
