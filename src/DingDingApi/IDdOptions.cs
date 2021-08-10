namespace DingDingApi
{
    public interface IDdOptions
    {
        string AgentId { get; set; }
        string AppKey { get; set; }
        string AppSecret { get; set; }
        string CallBalkUrl { get; set; }
    }
}