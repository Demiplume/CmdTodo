
namespace CmdTodo.Data
{
    public interface ICmdTodoStorage
    {
        public void ShowAll();
        public CmdTodoEntity Show(string partitionKey, string rowKey);
        public void Merge(CmdTodoEntity entity);
        public void Delete(CmdTodoEntity cmdTodo);
    }
}
