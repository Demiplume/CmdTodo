using CmdTodo.Data;
using MyTodo.Data.Data;
using System;

namespace CmdTodo.InConsole
{
    public class CmdTodo
    {
        private ICmdTodoData _cmdTodoData;
        public CmdTodo(ICmdTodoData cmdTodoData)
        {
            _cmdTodoData = cmdTodoData;
        }
        public void LineCommand(string[] args)
        {
            Console.WriteLine();
            if (args.Length == 0)
            {
                _cmdTodoData.ShowHelp();
            }
            else
            {
                switch (args[0].ToLower())
                {
                    case "new":
                        _cmdTodoData.CreateTodo(args);
                        break;
                    case "show":
                        _cmdTodoData.ShowTodos();
                        break;
                    case "delete":
                        _cmdTodoData.DeleteTodo(args);
                        break;
                    case "setconnectionstring":
                        SetConnString(args);
                        break;
                    case "settablename":
                        SetTableName(args);
                        break;
                    default:
                        _cmdTodoData.InvalidMenu(args[0]);
                        break;
                };
            }
        }
        private static void SetConnString(string[] args)
        {
            if (args.Length < 2)
                Console.WriteLine(
@"  <Error> This command requires a connection string
        >cmdTodo setConnString _connection string_");

            LocalConnStringData.SetConn(args[1]);
        }
        private static void SetTableName(string[] args)
        {
            if (args.Length < 2)
                Console.WriteLine(
@"  <Error> This command requires a table name
        >cmdTodo setTableName _tableName_");

            LocalConnStringData.SetTable(args[1]);
        }
    }
}
