using MyTodo.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CmdTodo.Data
{
    public class CmdTodoData : ICmdTodoData
    {
        public void ShowHelp()
        {
            Console.WriteLine(@"    <Help> Possible actions are:
        show,
        new, new -partitionKey- -rowKey- -content-,
        delete, delete -partitionKey- -rowKey-,
        setConnectionString, setTableName");
        }

        public void ShowTodos()
        {
            var todoData = InitCmdTodoData();
            if (todoData != null)
                todoData.ShowAll();
        }

        public void CreateTodo(string[] args)
        {
            var todoData = InitCmdTodoData();
            string partitionKey;
            string rowKey;
            string message;
            CmdTodoEntity myTodo;

            if (args.Length < 3)
            {
                Console.Write("partitionKey:");
                partitionKey = Console.ReadLine();
                Console.Write("rowKey:");
                rowKey = Console.ReadLine();
                Console.Write("Message:");
                message = Console.ReadLine();
            }
            else
            {
                List<String> arguments = args.ToList<string>();
                arguments.RemoveAt(0);
                partitionKey = arguments[0];
                arguments.RemoveAt(0);
                rowKey = arguments[0];
                arguments.RemoveAt(0);
                message = String.Join(" ", arguments);
            }

            myTodo = new CmdTodoEntity(partitionKey, rowKey, message);

            todoData.Merge(myTodo);
        }

        public void DeleteTodo(string[] args)
        {
            var todoData = InitCmdTodoData();
            string partitionKey;
            string rowKey;
            CmdTodoEntity myTodo;

            if (args.Length == 3)
            {
                partitionKey = args[1];
                rowKey = args[2];
            }
            else
            {
                Console.Write("partitionKey:");
                partitionKey = Console.ReadLine();
                Console.Write("rowKey:");
                rowKey = Console.ReadLine();
            }
            myTodo = todoData.Show(partitionKey, rowKey);
            todoData.Delete(myTodo);
        }

        public void InvalidMenu(string arg)
        {
            Console.WriteLine($"{arg} is not a valid action.");
            ShowHelp();
        }

        private static CmdTodoStorageCloud InitCmdTodoData()
        {
            string storageConn = LocalConnStringData.GetConn();
            string tableName = LocalConnStringData.GetTable();

            bool errorFound = false;
            if (String.IsNullOrEmpty(storageConn))
            {
                Console.WriteLine("Missing connection string");
                errorFound = true;
            }
            if (String.IsNullOrEmpty(tableName))
            {
                Console.WriteLine("Missing table name");
                errorFound = true;
            }
            if (errorFound)
                return null;
            else
                return new CmdTodoStorageCloud(storageConn, tableName);
        }
    }
}
