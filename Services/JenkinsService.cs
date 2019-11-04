using DashboardApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DashboardApi.Services {
    public class JenkinsService {
        private readonly IMongoCollection<Jenkins> _jenkins;
        private static readonly HttpClient httpClient = new HttpClient();

        public JenkinsService(IDashboardDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this._jenkins = database.GetCollection<Jenkins>(settings.JenkinsCollectionName);
        }

        public List<Jenkins> Get() =>
            this._jenkins.Find(jenkins => true).ToList();

        // public Jenkins Get(string id) =>
        //     this._jenkins.Find<Jenkins>(jenkins => jenkins.Id == id).FirstOrDefault();

        public Jenkins Get(string projectName) =>
            this._jenkins.Find<Jenkins>(jenkins => jenkins.ProjectName == projectName).FirstOrDefault();

        public Jenkins Create(Jenkins jenkin) {
            this._jenkins.InsertOne(jenkin);
            return jenkin;
        }

        public void Update(string projectName, Jenkins jenkIn) =>
            this._jenkins.ReplaceOne(jenkins => jenkins.ProjectName == projectName, jenkIn);

        public void Remove(Jenkins jenkIn) =>
            this._jenkins.DeleteOne(jenkins => jenkins.ProjectName == jenkIn.ProjectName);

        public void Remove(string projectName) =>
            this._jenkins.DeleteOne(jenkins => jenkins.ProjectName == projectName);

        public static async void Populate() {
            try {
                httpClient.DefaultRequestHeaders.Add("Authorization", "woodlee 1175ee02066c010dbfd71ec2888beb2f34");
                string responseBody = await httpClient.GetStringAsync("http://build/job/JDMS_JDMS_Step_1/api/json?tree=builds[number,color,status,timestamp,id,result]");
                System.Console.WriteLine(responseBody);
            } catch (HttpRequestException e) {
                System.Console.WriteLine("\nException Caught!");
                System.Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}