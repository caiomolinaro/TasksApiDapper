using System.ComponentModel.DataAnnotations.Schema;

namespace TasksApiDapper.Data;

[Table("Tasks")]
public record Task(int id, string Activity, string Status);