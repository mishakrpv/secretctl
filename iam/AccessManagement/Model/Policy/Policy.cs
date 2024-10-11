namespace AccessManagement.Model.Policy;

public class Policy
{
    public string Version { get; set; } = "10-10-2024";
    public string ProjectId { get; set; }
    public List<Statement> Statement { get; set; }
}