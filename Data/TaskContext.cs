using System.Data;

namespace TasksApiDapper.Data;

public class TaskContext
{
    public delegate Task<IDbConnection> GetConnection();
}
