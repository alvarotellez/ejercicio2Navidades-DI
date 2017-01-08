using ejercicio2.Models;
using Newtonsoft.Json;
using System;
using Windows.Web.Http;

namespace ejercicio2.ViewModels
{
    public class ManejadoraPersona
    {
        string urlAzure = "http://ejercicio2Navidades.azurewebsites.net/api";

        public async void BorrarPersona(int id)
        {
            HttpClient mihttpClient = new HttpClient();
            try
            {
                Uri url = new Uri(urlAzure + "/" + id);
                await mihttpClient.DeleteAsync(url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void GuardarPersona(clsPersona persona)
        {
            HttpClient mihttpClient = new HttpClient();
            Uri url = new Uri(urlAzure);

            try
            {
                string conversionJson = JsonConvert.SerializeObject(persona);
                IHttpContent contentPost = new HttpStringContent(conversionJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
                await mihttpClient.PostAsync(url, contentPost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void ActualizarPersona(clsPersona persona)
        {
            HttpClient mihttpClient = new HttpClient();
            Uri url = new Uri(urlAzure + "/" + persona.Id);
            try
            {
                string conversionJson = JsonConvert.SerializeObject(persona);
                IHttpContent contentput = new HttpStringContent(conversionJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
                await mihttpClient.PutAsync(url, contentput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}