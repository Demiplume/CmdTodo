using CmdTodo.Data;

namespace CmdTodo.InConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ICmdTodoData cmdTodoData = new CmdTodoData();

            var cmdTodo = new CmdTodo(cmdTodoData);
            cmdTodo.LineCommand(args);
        }
    }
}
