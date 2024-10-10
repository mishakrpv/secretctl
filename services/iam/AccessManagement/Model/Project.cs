using System.ComponentModel.DataAnnotations.Schema;

namespace AccessManagement.Model;

public class Project
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public List<Credentials> Credentials { get; set; }
    [NotMapped]
    public List<Policy.Policy> Policies { get; set; }
}