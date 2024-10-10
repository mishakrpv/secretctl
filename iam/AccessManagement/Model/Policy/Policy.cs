namespace AccessManagement.Model.Policy;

public class Policy
{
    public string Version { get; set; }
    public List<Statement> Statement { get; set; }
}