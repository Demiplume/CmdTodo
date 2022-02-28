using Microsoft.Azure.Cosmos.Table;

namespace CmdTodo.Data
{
    public class CmdTodoEntity : TableEntity
    {
        public string Message { get; set; }
        public CmdTodoEntity() {}
        public CmdTodoEntity(string partitionKey, string rowKey, string message)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Message = message;
        }

        public CmdTodoEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
    }
}
