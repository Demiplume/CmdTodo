using Microsoft.Azure.Cosmos.Table;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CmdTodo.Data
{
    public class CmdTodoStorageCloud : ICmdTodoStorage
    {
        private CloudTable _table;

        public CmdTodoStorageCloud(string storageConn, string tableName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConn);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            _table = tableClient.GetTableReference(tableName);
        }


        public void ShowAll()
        {
            var query = new TableQuery<CmdTodoEntity>();
            var result = _table.ExecuteQuery(query).ToList();

            foreach (var todo in result)
                Console.WriteLine($"partitionKey: {todo.PartitionKey}, rowKey: {todo.RowKey}, Message: {todo.Message}");
        }

        public CmdTodoEntity Show(string partitionKey, string rowKey)
        {
            var tmp = ShowAsync(partitionKey, rowKey);
            var tmp2 = tmp.Result;

            return tmp2;
        }

        internal async Task<CmdTodoEntity> ShowAsync(string partitionKey, string rowKey)
        {
            TableOperation getEntity = TableOperation.Retrieve<CmdTodoEntity>(partitionKey, rowKey);
            TableResult result = await _table.ExecuteAsync(getEntity);

            CmdTodoEntity myTodo = result.Result as CmdTodoEntity;

            return myTodo;
        }

        public void Merge(CmdTodoEntity entity)
        {
            MergeTodo(entity).Wait();
        }

        internal async Task MergeTodo(CmdTodoEntity cmdTodo)
        {
            TableOperation insertOrMergeOp = TableOperation.InsertOrMerge(cmdTodo);
            TableResult result = await _table.ExecuteAsync(insertOrMergeOp);
            CmdTodoEntity insertedNote = result.Result as CmdTodoEntity;

            Console.WriteLine("Todo added.");
        }

        public void Delete(CmdTodoEntity myTodo)
        {
            TableOperation delete = TableOperation.Delete(myTodo);
            _table.Execute(delete);
            Console.WriteLine("Deleted.");
        }
    }
}
