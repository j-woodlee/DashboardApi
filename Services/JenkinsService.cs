using DashboardApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DashboardApi.Services {
    public class JenkinsService {
        private readonly IMongoCollection<Jenkins> _jenkins;

        public JenkinsService(IDashboardDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this._jenkins = database.GetCollection<Jenkins>(settings.JenkinsCollectionName);
        }

        public List<Jenkins> Get() =>
            this._jenkins.Find(jenkins => true).ToList();

        public Jenkins Get(string id) =>
            this._jenkins.Find<Jenkins>(jenkins => jenkins.Id == id).FirstOrDefault();

        public Jenkins Create(Jenkins jenkin) {
            this._jenkins.InsertOne(jenkin);
            return jenkin;
        }

        public void Update(string id, Jenkins jenkIn) =>
            this._jenkins.ReplaceOne(jenkins => jenkins.Id == id, jenkIn);

        public void Remove(Jenkins jenkIn) =>
            this._jenkins.DeleteOne(jenkins => jenkins.Id == jenkIn.Id);

        public void Remove(string id) => 
            this._jenkins.DeleteOne(jenkins => jenkins.Id == id);
    }
}