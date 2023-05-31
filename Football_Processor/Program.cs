using System;
using Football_Processor;

class Program
{
    static UI _ui;

    static void Main(string[] args)
    {
        Run();
    }

    static void Run()
    {
        Console.Clear();
        Init();
        _ui.Start();
    }

    static void Init()
    {
        // Init of UI
        _ui = new UI();
    }
}
