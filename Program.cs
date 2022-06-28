// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;
using System.Xml;
using Newtonsoft.Json;
class Program{
    const string StorageAccountName = "absdemoresource1";
    const string ContainerName = "abs-resource-container1";
    const string StorageAccountKey = "DefaultEndpointsProtocol=https;AccountName=absdemoresource1;AccountKey=/scDwdUAq+wIa7QbJ0RDwALG4qHn+HHM2a0mevbuMQodQHMkK1HZ4rd+K4vpQoHBrs/t91V9Ok3R+AStrS4pYQ==;EndpointSuffix=core.windows.net";

static void Main(string[] args)
{    
    Program Program = new Program();
    Program.GetBlob("DCOU.xml",ContainerName);
}
public async Task<string> GetBlob(string name, string containerName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(StorageAccountKey);
            // this will allow us access to the storage container
           var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

           // this will allow us access to the file inside the container via the file name
           var blobClient = containerClient.GetBlobClient(name);
            MemoryStream memoryStream = new MemoryStream();
            blobClient.DownloadTo(memoryStream);
            memoryStream.Position=0;
            string content = new StreamReader(memoryStream).ReadToEnd();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            Console.WriteLine("JsonContent::"+jsonText);
           return blobClient.Uri.AbsoluteUri;
        }
}
