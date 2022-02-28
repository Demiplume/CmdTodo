namespace CmdTodo.Data
{
    public interface ICmdTodoData
    {
        public void ShowHelp();
        public void ShowTodos();
        public void DeleteTodo(string[] args);
        public void CreateTodo(string[] args);
        public void InvalidMenu(string arg);
    }
}
