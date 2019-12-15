using System;
using System.IO;
using System.Text;

namespace TankzClient
{
    public class TankzConsole : TextWriter
    {
        // TODO: refactor this mess
        private IConsoleChain network;

        private IConsoleChain singleton;
        private IConsoleChain factory;
        private IConsoleChain builder;
        private IConsoleChain adapter;
        private IConsoleChain decorator;
        private IConsoleChain command;
        private IConsoleChain bridge;

        private IConsoleChain iterator;
        private IConsoleChain template;
        private IConsoleChain state;
        private IConsoleChain flyweight;
        private IConsoleChain nullobj;
        private IConsoleChain mediator;
        private IConsoleChain interpreter;
        private IConsoleChain memento;
        private IConsoleChain proxy;
        private IConsoleChain composite;

        public TankzConsole()
        {
            // network
            network = new NetworkLogger();

            // design patterns
            singleton = new PatternLogger("singleton", ConsoleColor.Cyan);
            factory = new PatternLogger("factory", ConsoleColor.DarkRed);
            builder = new PatternLogger("builder", ConsoleColor.Green);
            adapter = new PatternLogger("adapter", ConsoleColor.Red);
            decorator = new PatternLogger("decorator", ConsoleColor.Yellow);
            command = new PatternLogger("command", ConsoleColor.DarkMagenta);
            bridge = new PatternLogger("bridge", ConsoleColor.DarkYellow);

            iterator = new PatternLogger("iterator", ConsoleColor.Magenta);
            template = new PatternLogger("template", ConsoleColor.Yellow);
            state = new PatternLogger("state", ConsoleColor.Blue);
            flyweight = new PatternLogger("flyweight", ConsoleColor.Cyan);
            nullobj = new PatternLogger("null object", ConsoleColor.DarkGreen);
            mediator = new PatternLogger("mediator", ConsoleColor.Green);
            interpreter = new PatternLogger("interpreter", ConsoleColor.DarkYellow);
            memento = new PatternLogger("memento", ConsoleColor.DarkMagenta);
            proxy = new PatternLogger("proxy", ConsoleColor.DarkCyan);
            composite = new PatternLogger("composite", ConsoleColor.DarkBlue);

            network
                //.SetNext(singleton)
                .SetNext(factory)
                .SetNext(builder)
                .SetNext(adapter)
                .SetNext(decorator)
                .SetNext(command)
                .SetNext(bridge)

                .SetNext(iterator)
                .SetNext(template)
                .SetNext(state)
                .SetNext(flyweight)
                .SetNext(nullobj)
                .SetNext(mediator)
                .SetNext(interpreter)
                .SetNext(memento)
                .SetNext(proxy)
                .SetNext(composite);

            standard = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
        }

        public override Encoding Encoding => Encoding.UTF8;

        private StreamWriter standard;

        public override void WriteLine(string message)
        {
            Console.SetOut(standard);
            network.Handle(message);
            Console.SetOut(this);
        }
    }
}
