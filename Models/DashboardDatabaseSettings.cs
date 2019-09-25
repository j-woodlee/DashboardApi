namespace DashboardApi.Models
{
    public class DashboardDatabaseSettings : IDashboardDatabaseSettings {
        public string JenkinsCollectionName { get; set; }
        public string JiraCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDashboardDatabaseSettings {
        string JenkinsCollectionName { get; set; }
        string JiraCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}