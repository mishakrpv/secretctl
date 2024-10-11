namespace AccessManagement.Model.Policy;

public class Statement
{
    public string Effect { get; set; }
    public string Principal { get; set; }
    public List<string> Action { get; set; }
    public string Resource { get; set; }
}