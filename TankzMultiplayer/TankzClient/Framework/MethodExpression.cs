using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Game;

namespace TankzClient.Framework
{
    class MethodExpression : IExpression
    {
        IExpression value;

        public MethodExpression(IExpression exp1)
        {
            value = exp1;
        }


        public string executeMethod()
        {
            string inputString = value.executeMethod();
            string className = inputString.Substring(0, inputString.IndexOf(' '));
            string methodName = inputString.Substring(inputString.IndexOf(' ') + 1, inputString.IndexOf('(') - inputString.IndexOf(' ') - 1);
            string methodVariables = inputString.Substring(inputString.IndexOf('(') + 1, inputString.Length - inputString.IndexOf('(') - 2);
            object[] variables = null;
            if (methodVariables == "")
            {
                
            }
            else
            {
                string[] fullVariables = methodVariables.Split(',');
                variables = new object[fullVariables.Length];
                for (int i = 0; i < variables.Length; i++)
                {
                    string[] line = fullVariables[i].Split(' ');
                    string varType = line[0];
                    switch (varType)
                    {
                        case "int":
                            variables[i] = Int32.Parse(line[1]);
                            break;
                        case "string":
                            variables[i] = line[1].ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            Console.WriteLine("Method name: " + methodName);
            Console.WriteLine("Method variables: " + methodVariables);

            switch (className)
            {
                case "SoundsPlayer":
                    Type type = typeof(SoundsPlayer);
                    MethodInfo theMethod = type.GetMethod(methodName);
                    string result = (string)theMethod.Invoke(SoundsPlayer.Instance, variables);
                    return result;
                default:
                    return "tokios klases nera";
            }
        }
    }
}
